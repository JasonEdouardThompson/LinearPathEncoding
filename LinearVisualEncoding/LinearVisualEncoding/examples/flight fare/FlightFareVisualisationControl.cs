using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearVisualEncoding.examples.flight_fare
{
    public partial class FlightFareVisualisationControl : UserControl
    {
        public FlightFareVisualisationControl()
        {
            InitializeComponent();

            depictionInterval_ComboBox.DataSource = depictionIntervalStrategies;
            depictionInterval_ComboBox.DisplayMember = "Description";

            colourScheme_comboBox.DataSource = colourSchemes;
            colourScheme_comboBox.DisplayMember = "Description";

            heightType_comboBox.SelectedIndex = 0;

            outputType_ComboBox.DataSource = outputOptions;
        }


        private static readonly string outputHTML = "output web page";
        private static readonly string outputImage = "output image";

        private static readonly string[] outputOptions = new string[] { outputHTML, outputImage };

        IDepictionIntervalStrategy[] depictionIntervalStrategies = new IDepictionIntervalStrategy[]
        {
            new EfficientDepictionIntervalStrategy(new TimeSpan(3, 20, 0)),
            new UniformDepictionIntervalStrategy()
        };

        public class ColourScheme
        {
            public String Description { get; set; }
            public AirlineTransferVisualAppearanceFactory factory;
            public delegate AirlineTransferVisualAppearance AirlineTransferVisualAppearanceFactory(AirlineEntityVisualAppearance eva, TransferGraph tg);

            public ColourScheme(String Description, AirlineTransferVisualAppearanceFactory factory)
            {
                this.Description = Description;
                this.factory = factory;
            }
        }

        ColourScheme[] colourSchemes = new ColourScheme[]
        {
            new ColourScheme("by departure airport", (eva, tg) => new AirlineTransferVisualAppearanceBySource(tg, eva)),
            new ColourScheme("by destination airport", (eva, tg) => new AirlineTransferVisualAppearanceByDestination(tg, eva)),
            new ColourScheme("by airline", (eva, tg) => new AirlineTransferVisualAppearanceByAirline(tg, eva))
        };

        private void heightType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.heightType_comboBox.Text == "Diagram Height")
            {
                this.heightValue_textbox.Text = "800";
            }
            else
            {
                this.heightValue_textbox.Text = "20";
            }
        }
      

        private static ALVisualParameters constructParametersLinearAL(
            TransferGraph tg, int width, int height, int ticks, AirlineEntityVisualAppearance eva, AirlineTransferVisualAppearance tva)
        {
            ALVisualParameters vp = new ALVisualParameters();

            vp.dp = new DiagramParameters();
            vp.dp.noTickMarks = ticks;
            vp.dp.labelString = "HH:mm";
            vp.dp.lineColour = Color.DarkGray;
            vp.dp.labelMargin = 5;
            vp.dp.font = LinearPathEncoding.tickFont;

            float margin = 10;
            float textHeight = TextRenderer.MeasureText("A", vp.dp.font).Height;

            vp.S = new LinearRectanglePlottingTransform(
                t0: tg.t0,
                t1: tg.t1,
                u0: 0,
                u1: 1,
                x0: margin,
                y0: margin + textHeight,
                x1: width - margin,
                y1: height);

            vp.eva = eva;

            vp.edp = new EntityDepictionParameters();
            vp.edp.colourSelection = vp.eva.colourSelection;
            vp.edp.thicknessSelection = vp.eva.thicknessSelection;

            vp.cems = new CommentEndMarkerStrategy(ls: 9, lf: 9, ds: 2, df: 2.5f, minS: 2);

            vp.tva = tva;

            vp.tdp = new TransferDepictionParameters();
            vp.tdp.colourSelection = vp.tva.colourSelection;
            vp.tdp.thicknessSelection = vp.tva.thicknessSelection;
            vp.tdp.ls = vp.cems.ls;
            vp.tdp.lf = vp.cems.lf;
            vp.tdp.startMarkerDrawer = vp.cems.drawStartMarker;
            vp.tdp.finishMarkerDrawer = vp.cems.drawFinishMarker;

            return vp;
        }

        private static void drawDiagramA(string filename, int width, int height, ALVisualParameters vp, Path[] paths, Bitmap etyColourKey, int s = 3)
        {
            drawDiagramA(width, height, vp, paths, etyColourKey, s).Save(filename, System.Drawing.Imaging.ImageFormat.Png);
        }
        private static Bitmap drawDiagramA(int width, int height, ALVisualParameters vp, Path[] paths, Bitmap etyColourKey, int s = 3)
        {
            //entity colour key

            foreach (var p in paths)
            {
                if (Utils.isBackgrounded(p))
                {
                    p.colour = ControlPaint.Light(p.colour, 1.4f);
                }
            }
            paths = paths.OrderBy(p => Utils.isBackgrounded(p) ? 0 : 1).ToArray();

            //draw the thickness key
            var trfThicknessKey = KeyHelper.generateThicknessKeyBitmapExtremes(
               vp.tva.minPrice,
               vp.tva.maxPrice,
               vp.tva.startPrice,
               vp.tva.stopPrice,
               vp.tva.minThickness,
               vp.tva.maxThickness,
               boxHeight: 100,
               margin: 5,
               noTicks: 5,
               title: "price",
               s: s);

            Bitmap[] keys = new Bitmap[]{
                 etyColourKey,
                 trfThicknessKey
            };
            int keyWidth = (int)keys.Max(k => Math.Ceiling(k.Width / (float)s));
            int keyMargin = 15;
            Bitmap b = new Bitmap(s * (width + keyMargin + keyWidth), s * height);
            using (Graphics g = Graphics.FromImage(b))
            using (SolidBrush br = new SolidBrush(Color.FromArgb(240, Color.Black)))
            {
                g.Clear(Color.White);
                g.ScaleTransform(s, s);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                LinearPathEncoding.drawTickMarks(g, vp.S, vp.dp);
                
                foreach (var p in paths)
                {
                    p.draw(g);
                    //draw the name of the airport on the path
                    if (p is EntityPath)
                    {
                        var ep = p as EntityPath;
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        g.DrawString(ep.entity.key, vp.dp.font, br, Math.Min(ep.x0 + 10, ep.x1), ep.y, sf);
                    }
                }

                //draw all of the keys
                float y = height - (keys.Sum(k => k.Height / (float)s) + keys.Count() * keyMargin);
                y *= 0.5f;
                for (int i = 0; i < keys.Length; i++)
                {
                    float h = keys[i].Height / (float)s;
                    g.DrawImage(keys[i], width + keyMargin, y, keys[i].Width / (float)s, h);
                    y += h + keyMargin;
                }
                return b;
            }
        }


        private void chooseDataset_button_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var tg = AirlineTransferGraph.LoadTransferGraph(openFileDialog.FileName,
                (depictionInterval_ComboBox.SelectedItem as IDepictionIntervalStrategy).compute);

            var attributes = tg.transfers.Select(tr => tr.attributes as AirlineTransferAttributes);

            minPrice_textBox.Text = String.Format("{0:0.0}", attributes.Min(attr => attr.price));
            maxPrice_textBox.Text = String.Format("{0:0.0}", attributes.Max(attr => attr.price));

            minTime_dateTimePicker.Value = tg.transfers.Min(tr => tr.ts);
            maxTime_dateTimePicker.Value = tg.transfers.Max(tr => tr.tf);

            airports_checkedListBox.Items.Clear();

            foreach (var attr in tg.entities.Select(a => a.attributes as AirlineEntityAttributes))
            {
                airports_checkedListBox.Items.Add(attr.name, true);
            }

        }

        private void generate_button_Click(object sender, EventArgs e)
        {

            if( outputType_ComboBox.SelectedItem == outputImage)
            {
                generateImage();
            }else if( outputType_ComboBox.SelectedItem == outputHTML)
            {
                generateHTML();
            }
        }

        private void generateHTML()
        {

            if (openFileDialog.FileName == "")
            {
                return;
            }

            var filename = openFileDialog.FileName;

            string folder = System.IO.Path.GetDirectoryName(filename) + "\\reps";
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }

            var name = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(filename));

            string basename = $"{folder}\\{name}({colourScheme_comboBox.Text})_web_page";

            if (!System.IO.Directory.Exists(basename))
            {
                System.IO.Directory.CreateDirectory(basename);
            }

            //copy across all of the resources            
            System.IO.File.WriteAllText($"{basename}\\flight_logic.js", LinearVisualEncoding.Properties.Resources.flight_logic);
            System.IO.File.WriteAllText($"{basename}\\flight_style.css", LinearVisualEncoding.Properties.Resources.flight_style);
            System.IO.File.WriteAllText($"{basename}\\jquery-1.11.3.js", LinearVisualEncoding.Properties.Resources.jquery_1_11_3);

            var tg = AirlineTransferGraph.LoadTransferGraph(openFileDialog.FileName,
                (depictionInterval_ComboBox.SelectedItem as IDepictionIntervalStrategy).compute);
            
            int width = int.Parse(diagramWidth_textBox.Text);
            int heightValue = int.Parse(heightValue_textbox.Text);
            int ticks = int.Parse(noTicks_textBox.Text);

            HeightType heightType = heightType_comboBox.Text == "Diagram Height" ? HeightType.DiagramHeight : HeightType.RowHeight;

            var q = LayoutOptimisation.ConstructGreedy(
                tg,
                _q => _q.costCombinedLI(),
                _e => -_e.transfers.Count);

            var height = heightType == HeightType.DiagramHeight ? heightValue : q.N * heightValue + heightValue;

            AirlineEntityVisualAppearance eva;
            int s = 1;

            //by source
            ALVisualParameters vp = constructParametersLinearAL(tg, width, height, ticks,
                eva: eva = new AirlineEntityVisualAppearance(tg, float.Parse(airportThickness_textBox.Text)),
                tva: (colourScheme_comboBox.SelectedItem as ColourScheme).factory(eva, tg));

            Path[] paths = LinearPathEncoding.constructPaths(tg, vp.S, q, vp.edp, vp.tdp);

            KeyHelper.generateCategoryKeyBitmap(
                vp.tva.getKeys(),
                vp.tva.getColors(),
                boxSize: 20,
                margin: 5,
                s: s).Save(basename + "\\etCol.png", System.Drawing.Imaging.ImageFormat.Png);

            KeyHelper.generateThicknessKeyBitmapExtremes(
               vp.tva.minPrice,
               vp.tva.maxPrice,
               vp.tva.startPrice,
               vp.tva.stopPrice,
               vp.tva.minThickness,
               vp.tva.maxThickness,
               boxHeight: 100,
               margin: 5,
               noTicks: 5,
               title: "price",
               s: s).Save(basename + "\\tThic.png", System.Drawing.Imaging.ImageFormat.Png);
            

            string template = @"<!DOCTYPE html>
                <html><head>
                <link href=""flight_style.css"" rel=""stylesheet"" />
                <script src=""jquery-1.11.3.js""></script>
                <script src=""flight_logic.js""></script>
                  </head><body>
                <div class=""visual_index""><svg width=""{0}"" height=""{1}"" >
                <g id=""up""></g>
                <g id=""base"">{2}</g>
                <g id=""down""></g>
                </svg></div>
                <div class=""keys""><img src=""etCol.png""/><br/><img src=""tThic.png""/></div>

                <form>
                    min price:<br/>
                    <input type='text' id='min_price'><br/>
                    max price:<br/>
                    <input type = 'text' id = 'max_price' ><br/>
<!--
                    min time:<br/>
                    <input type='text' id='min_time'><br/>
                    max time:<br/>
                    <input type = 'text' id = 'max_time' ><br/> -->
                    Airports:<br/>{3}
                    <button id='filter' type='button'>filter</button>
                    <button id='reset' type='button'>reset</button>
                </ form >
   
                   </body></html>";

            string htmlIndexFileName = basename + "\\index.html";

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(htmlIndexFileName, append: false, encoding: Encoding.UTF8))
            {
                SVGHelper svg = new SVGHelper();

                Font font = LinearPathEncoding.tickFont;

                var dp = vp.dp;
                var S = vp.S;

                using (Pen p = new Pen(dp.lineColour))
                {
                    float xCutoff = vp.S.x0;

                    for (int i = 0; i < dp.noTickMarks; i++)
                    {
                        float u = (float)i / (dp.noTickMarks - 1);

                        DateTime t = S.t0.AddMilliseconds(u * S.d.TotalMilliseconds);
                        var p0 = S.transform(t, -0.1f);
                        var p1 = S.transform(t, 1);

                        svg.DrawLine(p0.X, p0.Y, p1.X, p1.Y, "lightgrey", 1);

                        float x = S.transformX(t);
                        if (x < xCutoff)
                        {
                            continue;
                        }

                        string label = t.ToString(dp.labelString);
                        float w = 40;
                        if (x + w > width && x - w > xCutoff)
                        {
                            //g.DrawString(label, font, Brushes.Black, new PointF(x - w, 0));
                            //svg.DrawText(x - w, 15, label, "black");
                        }
                        else
                        {
                            svg.DrawText(x, 15, label, "black");
                        }
                        xCutoff = x + w + dp.labelMargin;
                    }
                }

                foreach (var p in paths)
                {
                    var colour = "#" + (p.colour.ToArgb() & 0x00FFFFFF).ToString("X6");
                    var w = p.thickness;

                    if (p is EntityPath)
                    {

                        var pe = p as EntityPath;

                        var attributes = pe.entity.attributes as AirlineEntityAttributes;

                        var xmlAttributes = new Dictionary<String, String>();
                        xmlAttributes.Add("name", attributes.name);
                        xmlAttributes.Add("type", "entity");

                        svg.startGroup(xmlAttributes);

                        //base
                        svg.DrawLine(
                            pe.x0, pe.y,
                            pe.x1, pe.y,
                            colour,
                            w);

                        svg.DrawText(pe.x0 + w / 4, pe.y + w/4, attributes.name, "rgba(0,0,0,150)");

                        svg.endGroup();

                    }
                    else
                    {

                        var pt = (p as TransferPath);
                        var attributes = pt.transfer.attributes as AirlineTransferAttributes;

                        var xmlAttributes = new Dictionary<String, String>();
                        xmlAttributes.Add("price", "" + attributes.price);
                        xmlAttributes.Add("start", "" + Utils.ConvertToUnixTimeMilliSeconds(pt.transfer.ts));
                        xmlAttributes.Add("finish", "" + Utils.ConvertToUnixTimeMilliSeconds(pt.transfer.tf));
                        xmlAttributes.Add("source", (pt.transfer.source.attributes as AirlineEntityAttributes).name);
                        xmlAttributes.Add("destination", (pt.transfer.destination.attributes as AirlineEntityAttributes).name);
                        xmlAttributes.Add("type", "transfer");
                        xmlAttributes.Add("colour", "#" + (p.colour.ToArgb() & 0x00FFFFFF).ToString("X6"));

                        svg.startGroup(xmlAttributes);
                        //make it bigger so its easier for tool tips
                        svg.DrawLine(
                            pt.connectionPointA.X, pt.connectionPointA.Y,
                            pt.connectionPointB.X, pt.connectionPointB.Y,
                            "transparent",
                            2 * vp.tva.maxThickness);

                        //base
                        svg.DrawLine(
                            pt.connectionPointA.X, pt.connectionPointA.Y,
                            pt.connectionPointB.X, pt.connectionPointB.Y,
                            colour,
                            w);

                        //finish
                        svg.DrawArrowHead(
                            pt.focusPointA.X, pt.focusPointA.Y,
                            pt.focusPointB.X, pt.focusPointB.Y,
                            w + 2 * vp.cems.df,
                            vp.cems.lf,
                            colour);

                        //start
                        svg.DrawArrowHead(
                            pt.focusPointB.X, pt.focusPointB.Y,
                            pt.focusPointA.X, pt.focusPointA.Y,
                            Math.Min(Math.Max(w - 2 * vp.cems.ds, vp.cems.minS), w),
                            vp.cems.ls,
                            colour);

                        svg.endGroup();

                    }
                }

                StringBuilder sb = new StringBuilder();

                foreach (var e in tg.entities)
                {
                    sb.Append($"<input airport='{(e.attributes as AirlineEntityAttributes).name}' type='checkbox' checked='checked'/>{(e.attributes as AirlineEntityAttributes).name}<br/>");
                }

                fs.Write(String.Format(template,
                    width,
                    height,
                    svg.ToString(),
                    sb.ToString()));
            }


            System.Diagnostics.Process.Start(htmlIndexFileName);
        }

        private void generateImage()
        {
            if (openFileDialog.FileName == "")
            {
                return;
            }

            string folder = System.IO.Path.GetDirectoryName(openFileDialog.FileName) + "\\reps";

            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }


            var tg = AirlineTransferGraph.LoadTransferGraph(openFileDialog.FileName,
                (depictionInterval_ComboBox.SelectedItem as IDepictionIntervalStrategy).compute);

            double minPrice = double.Parse(minPrice_textBox.Text);
            double maxPrice = double.Parse(maxPrice_textBox.Text);

            DateTime minTime = minTime_dateTimePicker.Value;
            DateTime maxTime = maxTime_dateTimePicker.Value;



            filter(tg,
                (r) => {
                    var attrs = (r.attributes as AirlineTransferAttributes);
                    return !(r.ts >= minTime && r.tf <= maxTime && attrs.price >= minPrice && attrs.price <= maxPrice);
                },
                (ety) => {
                    var attrs = (ety.attributes as AirlineEntityAttributes);
                    int index = airports_checkedListBox.Items.IndexOf(attrs.name);
                    return !airports_checkedListBox.GetItemChecked(index);
                });

            int width = int.Parse(diagramWidth_textBox.Text);
            int heightValue = int.Parse(heightValue_textbox.Text);
            int ticks = int.Parse(noTicks_textBox.Text);

            HeightType heightType = heightType_comboBox.Text == "Diagram Height" ? HeightType.DiagramHeight : HeightType.RowHeight;

            var q = LayoutOptimisation.ConstructGreedy(
                tg,
                _q => _q.costCombinedLI(),
                _e => -_e.transfers.Count);

            var height = heightType == HeightType.DiagramHeight ? heightValue : q.N * heightValue + heightValue;

            AirlineEntityVisualAppearance eva;
            int s = 3;

            string basename = folder + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName));
            basename += String.Format("({0})", colourScheme_comboBox.Text);
            //by source
            ALVisualParameters vp = constructParametersLinearAL(tg, width, height, ticks,
                eva: eva = new AirlineEntityVisualAppearance(tg, float.Parse(airportThickness_textBox.Text)),
                tva: (colourScheme_comboBox.SelectedItem as ColourScheme).factory(eva, tg));

            Path[] paths = LinearPathEncoding.constructPaths(tg, vp.S, q, vp.edp, vp.tdp);

            var colourKey = KeyHelper.generateCategoryKeyBitmap(
                vp.tva.getKeys(),
                vp.tva.getColors(),
                boxSize: 20,
                margin: 5,
                s: s);

            drawDiagramA(basename + ".png", width, height, vp, paths, colourKey, s);

            System.Diagnostics.Process.Start(basename + ".png");
        }
        private static void filter(TransferGraph tg, Predicate<Transfer> transferPredicate, Predicate<Entity> entityPredicate )
        {

            foreach (var e in tg.entities)
            {
                (e.attributes as IBackgroundable).isBackgrounded = entityPredicate?.Invoke(e) ?? false;
            }

            foreach (var r in tg.transfers)
            {
                if((r.source.attributes as IBackgroundable).isBackgrounded ||
                    (r.destination.attributes as IBackgroundable).isBackgrounded)
                {
                    (r.attributes as IBackgroundable).isBackgrounded = true;
                    continue;
                }
                (r.attributes as IBackgroundable).isBackgrounded = transferPredicate?.Invoke(r) ?? false;
            }

            foreach (var e in tg.entities)
            {
                var attrs = (e.attributes as IBackgroundable);
                if (attrs.isBackgrounded)
                {
                    continue;
                }
                attrs.isBackgrounded = true;
                foreach (var r in e.transfers.Select( r => (r.attributes as IBackgroundable)))
                {
                    if (!r.isBackgrounded)
                    {
                        attrs.isBackgrounded = false;
                        break;
                    }
                }
            }
        }
    }
}
