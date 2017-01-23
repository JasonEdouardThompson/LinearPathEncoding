using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinearVisualEncoding.examples.web_comments
{
    public partial class WebCommentControl : UserControl
    {
        public WebCommentControl()
        {
            InitializeComponent();

            outputType_ComboBox.DataSource = outputOptions;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private static readonly string outputHTML = "output web page";
        private static readonly string outputImage = "output image";

        private static readonly string[] outputOptions = new string[] { outputHTML, outputImage };

        private static VisualParameters constructParametersLinear(TransferGraph tg, int width, int height, int ticks)
        {
            VisualParameters vp = new VisualParameters();

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

            vp.eva = new CommentEntityVisualAppearance(tg);

            vp.edp = new EntityDepictionParameters();
            vp.edp.colourSelection = vp.eva.colourSelection;
            vp.edp.thicknessSelection = vp.eva.thicknessSelection;

            vp.cems = new CommentEndMarkerStrategy(ls: 9, lf: 9, ds: 2, df: 1, minS: 2);

            vp.tva = new CommentTransferVisualAppearance(tg);

            vp.tdp = new TransferDepictionParameters();
            vp.tdp.colourSelection = vp.tva.colourSelection;
            vp.tdp.thicknessSelection = vp.tva.thicknessSelection;
            vp.tdp.ls = vp.cems.ls;
            vp.tdp.lf = vp.cems.lf;
            vp.tdp.startMarkerDrawer = vp.cems.drawStartMarker;
            vp.tdp.finishMarkerDrawer = vp.cems.drawFinishMarker;

            return vp;
        }

        private static VisualParameters constructParametersAdaptive(TransferGraph tg, int width, int height, int ticks)
        {
            VisualParameters vp = new VisualParameters();

            vp.dp = new DiagramParameters();
            vp.dp.noTickMarks = ticks;
            vp.dp.labelString = "HH:mm";
            vp.dp.lineColour = Color.DarkGray;
            vp.dp.labelMargin = 5;
            vp.dp.font = LinearPathEncoding.tickFont;

            float margin = 10;
            float textHeight = TextRenderer.MeasureText("A", vp.dp.font).Height;

            vp.S = new AdaptivePlottingTransform(
                intervals: ticks - 1,
                tg: tg,
                t0: tg.t0,
                t1: tg.t1,
                u0: 0,
                u1: 1,
                x0: margin,
                y0: margin + textHeight,
                x1: width - margin,
                y1: height);

            System.Diagnostics.Debug.WriteLine(vp.S.ToString());
            vp.eva = new CommentEntityVisualAppearance(tg);

            vp.edp = new EntityDepictionParameters();
            vp.edp.colourSelection = vp.eva.colourSelection;
            vp.edp.thicknessSelection = vp.eva.thicknessSelection;

            vp.cems = new CommentEndMarkerStrategy(ls: 9, lf: 9, ds: 2, df: 1, minS: 2);

            vp.tva = new CommentTransferVisualAppearance(tg);

            vp.tdp = new TransferDepictionParameters();
            vp.tdp.colourSelection = vp.tva.colourSelection;
            vp.tdp.thicknessSelection = vp.tva.thicknessSelection;
            vp.tdp.ls = vp.cems.ls;
            vp.tdp.lf = vp.cems.lf;
            vp.tdp.startMarkerDrawer = vp.cems.drawStartMarker;
            vp.tdp.finishMarkerDrawer = vp.cems.drawFinishMarker;

            return vp;
        }

        private static void drawDiagram(string filename, int width, int height, VisualParameters vp, Path[] paths, int s = 3)
        {
            drawDiagram(width, height, vp, paths, s).Save(filename, System.Drawing.Imaging.ImageFormat.Png);
        }

        private static Bitmap drawDiagram(int width, int height, VisualParameters vp, Path[] paths, int s = 3)
        {
            //entity colour key
            var etyColourKey = KeyHelper.generateCategoryKeyBitmap(
                new string[] { "generator", "provoker" },
                new Color[] { vp.eva.generatorColour, vp.eva.provokerColour },
                boxSize: 20,
                margin: 5,
                s: s);//.Save(basename + "_eCol.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            //entity Thickness key
            var etyThicknessKey = KeyHelper.generateThicknessKeyBitmap(
                vp.eva.minExpectedScore,
                vp.eva.maxExpectedScore,
                vp.eva.minThickness,
                vp.eva.maxThickness,
                boxHeight: 100,
                margin: 5,
                noTicks: 5,
                title: "Expected Abs. Score",
               s: s);//.Save(basename + "_eThic.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            //Transfer colour key
            var trfColourKey = KeyHelper.generateColourKeyBitmap(
                vp.tva.values,
                vp.tva.colours,
                boxWidth: 15,
                boxHeight: 100,
                margin: 5,
                title: "Comment Score",
               s: s);//.Save(basename + "_tCol.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            //Transfer Thickness key
            var trfThicknessKey = KeyHelper.generateThicknessKeyBitmap(
               vp.tva.minSize,
               vp.tva.maxSize,
               vp.tva.minThickness,
               vp.tva.maxThickness,
               boxHeight: 100,
               margin: 5,
               noTicks: 5,
               title: "No. Words",
               s: s);//.Save(basename + "_tThic.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            Bitmap[] keys = new Bitmap[]{
                 etyColourKey,
                 etyThicknessKey,
                 trfColourKey,
                 trfThicknessKey
            };
            int keyWidth = (int)keys.Max(k => Math.Ceiling(k.Width / (float)s));
            int keyMargin = 15;
            Bitmap b = new Bitmap(s * (width + keyMargin + keyWidth), s * height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.White);
                g.ScaleTransform(s, s);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                LinearPathEncoding.drawTickMarks(g, vp.S, vp.dp);

                foreach (var p in paths)
                {
                    p.draw(g);
                }
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

        public delegate VisualParameters ConstructVisualParameters(TransferGraph tg, int width, int height, int ticks);

        private static String basicVisEnc(string filename, 
            string label,
            int width, 
            int heightValue, 
            HeightType heightType, 
            int ticks, 
            ConstructVisualParameters constructVisualParameters)
        {
            TransferGraph tg = CommentTransferGraph.loadCommentTransferGraph(filename);

            var greedyConstructionArrangement = LayoutOptimisation.ConstructGreedy(
                tg,
                _q => _q.costCombined(),
                _e => -_e.transfers.Count);

            var greedyIterationArrangement = new Arrangement(greedyConstructionArrangement);
            LayoutOptimisation.iterateGreedy(
                greedyIterationArrangement,
                _q => _q.costCombined(),
                LayoutOptimisation.iterateRandomOrder(tg, tg.entities.Length * 4));

            var height = heightType == HeightType.DiagramHeight ? heightValue : Math.Max(greedyConstructionArrangement.N, greedyIterationArrangement.N) * heightValue + heightValue;

            //VisualParameters vp = constructParametersAdaptive(tg, width, height, ticks);
            VisualParameters vp = constructVisualParameters(tg, width, height, ticks);

            var paths = LinearPathEncoding.constructPaths(tg, vp.S, greedyConstructionArrangement, vp.edp, vp.tdp);
            var paths_i = LinearPathEncoding.constructPaths(tg, vp.S, greedyIterationArrangement, vp.edp, vp.tdp);

            string folder = System.IO.Path.GetDirectoryName(filename) + "\\reps";
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }

            var name = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(filename));

            string basename = folder + "\\" + name;

            drawDiagram(basename + "." + label + ".constructed.png", width, height, vp, paths);
            drawDiagram(basename + "." + label + ".iterated.png", width, height, vp, paths_i);

            return basename + "." + label + ".iterated.png";
        }

        public delegate void ImageOutputted(int imagesOutputted, int totalImages);
        private static String basicVisEncSequence(string filename, int width, int heightValue, HeightType heightType, int ticks, int desiredNoClicks, Func<Transfer,double> commentOrderProjection, ImageOutputted imageOuttputed)
        {
            string folder = System.IO.Path.GetDirectoryName(filename) + "\\reps";
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }

            string basename = folder + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(filename));


            TransferGraph tg = CommentTransferGraph.loadCommentTransferGraph(filename);

            var greedyConstructionArrangement = LayoutOptimisation.ConstructGreedy(
                tg,
                _q => _q.costCombined(),
                _e => -_e.transfers.Count);

            var greedyIterationArrangement = new Arrangement(greedyConstructionArrangement);
            LayoutOptimisation.iterateGreedy(
                greedyIterationArrangement,
                _q => _q.costCombined(),
                LayoutOptimisation.iterateRandomOrder(tg, tg.entities.Length * 4));
            
            var height = heightType == HeightType.DiagramHeight ? heightValue : Math.Max(greedyConstructionArrangement.N, greedyIterationArrangement.N) * heightValue + heightValue;

            VisualParameters vp = constructParametersAdaptive(tg, width, height, ticks);

            //group transfers by thread
            var groups = tg.transfers.GroupBy(r => (r.attributes as CommentTransferAttributes).thread);
            var groupsArray = groups.OrderByDescending(gr => gr.Max(commentOrderProjection)).ToArray();
            int noClicks = Math.Min(groupsArray.Length, desiredNoClicks);
            System.Diagnostics.Debug.WriteLine("all" + tg.transfers.Max(commentOrderProjection));
            for (int i = 0; i < noClicks; i++)
            {
                System.Diagnostics.Debug.WriteLine(groupsArray[i].Max(commentOrderProjection));
                if (i > 0)
                {
                    Utils.backgrounGroup(tg, groupsArray, i - 1);
                }
                var paths = LinearPathEncoding.constructPaths(tg, vp.S, greedyIterationArrangement, vp.edp, vp.tdp);

                foreach (var p in paths)
                {
                    if (Utils.isBackgrounded(p))
                    {
                        p.colour = ControlPaint.Light(p.colour, 1.5f);
                    }
                }
                paths = paths.OrderBy(p => Utils.isBackgrounded(p) ? 0 : 1).ToArray();

                int s = 3;
                var b = drawDiagram(width, height, vp, paths, s);
                //find the click path for each group
                // it is located in group i
                var clickTransfer = groupsArray[i].OrderByDescending(commentOrderProjection).First();
                System.Diagnostics.Debug.WriteLine("click" + commentOrderProjection(clickTransfer));
                var clickPath = (TransferPath)paths.Where(p => p is TransferPath).First(p => p is TransferPath && ((TransferPath)p).transfer == clickTransfer);

                var g = Graphics.FromImage(b);
                g.ScaleTransform(s, s);
                using (Brush br = new SolidBrush(Color.FromArgb(75, Color.Orange)))
                {
                    g.FillRectangle(br, Rectangle.Round(clickPath.bounds));
                }
                g.DrawRectangle(Pens.Red, Rectangle.Round(clickPath.bounds));

                b.Save(String.Format("{0}.{1}.png", basename, i), System.Drawing.Imaging.ImageFormat.Png);

                imageOuttputed?.Invoke(i + 1, noClicks);
            }
            return folder;
        }

        private int width, heightValue, ticks, desiredNoClicks;
        private Func<Transfer, double> commentOrderProjection;
        private HeightType heightType;
        private string selectedOutputType = null;

        private void prepareForTask()
        {
            groupBox.Enabled = false;
            progressBar.Visible = true;

            width = int.Parse(diagramWidth_textbox.Text);
            heightValue = int.Parse(heightValue_textbox.Text);
            ticks = int.Parse(numberOfTicks_textbox.Text);
            desiredNoClicks = int.Parse(desiredNoClicks_textbox.Text);
            commentOrderProjection = (commentBrowsingOrder_comboBox.SelectedItem as BrowsingOrder).projection;
            selectedOutputType = outputType_ComboBox.SelectedItem as String;
            heightType = heightType_comboBox.Text == "Diagram Height" ? HeightType.DiagramHeight : HeightType.RowHeight;
        }
        private void chooseSingleDataset_button_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            prepareForTask();

            progressBar.Style = ProgressBarStyle.Marquee;
            singleDatasetWorker.RunWorkerAsync();
        }

        private string generateHTML()
        {
            string template = @"<!DOCTYPE html>
<html><head>
<link href=""comment_style.css"" rel=""stylesheet"" />
<script src=""jquery-1.11.3.js""></script>
<script src=""comment_logic.js""></script>
  </head><body>
<div class=""visual_index""><svg width=""{0}"" height=""{1}"" >
<g id=""up""></g>
<g id=""base"">{2}</g>
<g id=""down""></g>
</svg></div>
<div class=""keys""><img src=""eCol.png""/></br><img src=""eThic.png""/></br><img src=""tCol.png""/></br><img src=""tThic.png""/></div>
<div class=""comments"">{3}</div>

</body></html>";

            var filename = openFileDialog.FileName;

            TransferGraph tg = CommentTransferGraph.loadCommentTransferGraph(filename);

            var greedyConstructionArrangement = LayoutOptimisation.ConstructGreedy(
                tg,
                _q => _q.costCombined(),
                _e => -_e.transfers.Count);

            var greedyIterationArrangement = new Arrangement(greedyConstructionArrangement);
            LayoutOptimisation.iterateGreedy(
                greedyIterationArrangement,
                _q => _q.costCombined(),
                LayoutOptimisation.iterateRandomOrder(tg, tg.entities.Length * 4));

            var height = heightType == HeightType.DiagramHeight ? heightValue : Math.Max(greedyConstructionArrangement.N, greedyIterationArrangement.N) * heightValue + heightValue;
            
            VisualParameters vp = constructParametersAdaptive(tg, width, height, ticks);
            
            var paths_i = LinearPathEncoding.constructPaths(tg, vp.S, greedyIterationArrangement, vp.edp, vp.tdp);

            string folder = System.IO.Path.GetDirectoryName(filename) + "\\reps";
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }

            var name = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetFileNameWithoutExtension(filename));

            string basename = folder + "\\" + name + "_web_page";

            if (!System.IO.Directory.Exists(basename))
            {
                System.IO.Directory.CreateDirectory(basename);
            }

            int s = 1;

            //entity colour key
            KeyHelper.generateCategoryKeyBitmap(
                new string[] { "generator", "provoker" },
                new Color[] { vp.eva.generatorColour, vp.eva.provokerColour },
                boxSize: 20,
                margin: 5,
                s: s).Save(basename + "\\eCol.png", System.Drawing.Imaging.ImageFormat.Png);

            //entity Thickness key
            KeyHelper.generateThicknessKeyBitmap(
                vp.eva.minExpectedScore,
                vp.eva.maxExpectedScore,
                vp.eva.minThickness,
                vp.eva.maxThickness,
                boxHeight: 100,
                margin: 5,
                noTicks: 5,
                title: "Expected Abs. Score",
               s: s).Save(basename + "\\eThic.png", System.Drawing.Imaging.ImageFormat.Png);

            //Transfer colour key
            KeyHelper.generateColourKeyBitmap(
                vp.tva.values,
                vp.tva.colours,
                boxWidth: 15,
                boxHeight: 100,
                margin: 5,
                title: "Comment Score",
               s: s).Save(basename + "\\tCol.png", System.Drawing.Imaging.ImageFormat.Png);

            //Transfer Thickness key
            KeyHelper.generateThicknessKeyBitmap(
               vp.tva.minSize,
               vp.tva.maxSize,
               vp.tva.minThickness,
               vp.tva.maxThickness,
               boxHeight: 100,
               margin: 5,
               noTicks: 5,
               title: "No. Words",
               s: s).Save(basename + "\\tThic.png", System.Drawing.Imaging.ImageFormat.Png);


            string htmlIndexFileName = basename + "\\index.html";

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(htmlIndexFileName, append:false, encoding: Encoding.UTF8))
            {
                SVGHelper svg = new SVGHelper();

                Font font = LinearPathEncoding.tickFont;

                string lineColour = "lightgray";

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

                        svg.DrawLine( p0.X, p0.Y, p1.X, p1.Y, "lightgrey", 1);
                        
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

                foreach (var p in paths_i)
                {
                    var colour = "#" + (p.colour.ToArgb() & 0x00FFFFFF).ToString("X6");
                    var w = p.thickness;

                    if ( p is EntityPath) {

                        var pe = p as EntityPath;

                        var attributes = pe.entity.attributes as CommentEntityAttributes;

                        var xmlAttributes = new Dictionary<String, String>();
                        xmlAttributes.Add("author", attributes.name);
                        xmlAttributes.Add("type", "entity");

                        svg.startGroup(xmlAttributes);

                        //base
                        svg.DrawLine(
                            pe.x0, pe.y,
                            pe.x1, pe.y,
                            colour,
                            w);

                        svg.endGroup();

                    } else {
                        
                        var pt = (p as TransferPath);
                        var attributes = pt.transfer.attributes as CommentTransferAttributes;

                        var xmlAttributes = new Dictionary<String, String>();
                        xmlAttributes.Add("href", attributes.href);
                        xmlAttributes.Add("thread", attributes.thread);
                        xmlAttributes.Add("type", "transfer");

                        svg.startGroup(xmlAttributes);
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
                            Math.Abs(pt.focusPointA.Y - pt.connectionPointA.Y),
                            colour);

                        //start
                        svg.DrawArrowHead(
                            pt.focusPointB.X, pt.focusPointB.Y,
                            pt.focusPointA.X, pt.focusPointA.Y,
                            Math.Min(Math.Max(w - 2 * vp.cems.ds, vp.cems.minS), w),
                            Math.Abs(pt.focusPointB.Y - pt.connectionPointB.Y),
                            colour);

                        svg.endGroup();

                    }
                }

                StringBuilder sb = new StringBuilder();

                var comments = tg.transfers
                    .OrderBy(t => t.ts)
                    .Select(tr => tr.attributes as CommentTransferAttributes)
                    .ToList();

                sb.Append("<ul>");
                while (comments.Count > 0)
                {
                    var top = comments[0];
                    comments.RemoveAt(0);

                    processComments(sb, top, comments);
                }

                sb.Append("</ul>");
                fs.Write(String.Format(template,
                    width, 
                    height, 
                    svg.ToString(),
                    sb.ToString()));
            }


            System.IO.File.WriteAllText($"{basename}\\comment_logic.js", LinearVisualEncoding.Properties.Resources.comment_logic);
            System.IO.File.WriteAllText($"{basename}\\comment_style.css", LinearVisualEncoding.Properties.Resources.comment_style);
            System.IO.File.WriteAllText($"{basename}\\jquery-1.11.3.js", LinearVisualEncoding.Properties.Resources.jquery_1_11_3);


            return htmlIndexFileName;
        }

        private static void processComments(StringBuilder sb, CommentTransferAttributes top, List<CommentTransferAttributes> comments)
        {
            sb.Append("<li>");
            sb.AppendFormat($@"<span thread=""{top.thread}"" class=""single-comment"" id=""{top.href}""><span class=""meta""><em>{(top.transfer.source.attributes as CommentEntityAttributes).name}</em>, score: {top.score}, date: {top.transfer.ts}</span><br/>{top.message}</span><br/>");
            sb.AppendFormat($@"<button href='{top.href}' onclick=""toggleComment(this, '{top.href}')"">hide comment</button>");
            sb.AppendFormat($@"<button onclick=""toggleThread(this, '{top.thread}')"">toggle thread</button>");

            var hasChildren = (top.children.Count > 0);

            if (hasChildren)
            {
                sb.Append("<ul>");
            }
            
            foreach (var child in top.children)
            {
                comments.Remove(child);

                processComments(sb, child, comments);
            }

            if (hasChildren)
            {
                sb.Append("</ul>");
            }

            sb.Append("</li>");
        }


        private void chooseFolder_button_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            prepareForTask();

            progressBar.Style = ProgressBarStyle.Continuous;

            folderDatasetWorker.RunWorkerAsync();
        }
        public class BrowsingOrder
        {
            public string description { get; set; }
            public Func<Transfer, double> projection;

            public BrowsingOrder(string description, Func<Transfer, double> projection)
            {
                this.description = description;
                this.projection = projection;
            }
        }
        

        private void chooseDatasetForSequence_button_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            prepareForTask();

            progressBar.Style = ProgressBarStyle.Blocks;

            sequenceDatasetWorker.RunWorkerAsync();
        }

        private void WebCommentControl_Load(object sender, EventArgs e)
        {
            this.heightType_comboBox.SelectedIndex = 0;

            List<BrowsingOrder> browsingOrders = new List<BrowsingOrder>()
            {
                new BrowsingOrder("by newest", (transfer) => -(double)transfer.ts.Subtract(new DateTime(2000,1,1)).TotalSeconds),
                new BrowsingOrder("by oldest", (transfer) => (double)transfer.ts.Subtract(new DateTime(2000,1,1)).TotalSeconds),
                new BrowsingOrder("by highest score", (transfer)=> (transfer.attributes as CommentTransferAttributes).score),
                new BrowsingOrder("by lowest score", (transfer)=> -(transfer.attributes as CommentTransferAttributes).score),
                new BrowsingOrder("by longest", (transfer)=> (transfer.attributes as CommentTransferAttributes).size),
                new BrowsingOrder("by shortest", (transfer)=> -(transfer.attributes as CommentTransferAttributes).size),
            };

            commentBrowsingOrder_comboBox.DataSource = browsingOrders;
            commentBrowsingOrder_comboBox.DisplayMember = "description";
        }

        private void heightType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( this.heightType_comboBox.Text == "Diagram Height" )
            {
                this.heightValue_textbox.Text = "800";
            }else
            {
                this.heightValue_textbox.Text = "20";
            }
        }

        private void datasetWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void folderDatasetWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string folder = folderBrowserDialog.SelectedPath;
            var files = System.IO.Directory.EnumerateFiles(folder, "*.etm.txt");
            
            int i = 0;
            float count = files.Count();

            foreach (var f in files)
            {
                basicVisEnc(f, "", width, heightValue, heightType, ticks, constructParametersAdaptive);
         
                (sender as BackgroundWorker).ReportProgress((int)(100.0f * i++ / count));

                if( e.Cancel)
                {
                    break;
                }
            }
            System.Diagnostics.Process.Start(folder + "//reps");
        }

        private void sequenceDatasetWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var output = basicVisEncSequence(
                openFileDialog.FileName,
                width, heightValue, heightType, 
                ticks,
                desiredNoClicks,
                commentOrderProjection,
                (i, n)=> { (sender as BackgroundWorker).ReportProgress((int)(100.0f * i++ / n)); });

            var folder = System.IO.Path.GetDirectoryName(output);
            System.Diagnostics.Process.Start(folder + "//reps");
        }
        
        private void singleDatasetWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            basicVisEnc(openFileDialog.FileName, "linear", width, heightValue, heightType, ticks, constructParametersLinear);

            string output = "";

            if (selectedOutputType == outputImage)
            {
                output = basicVisEnc(openFileDialog.FileName, "adaptive", width, heightValue, heightType, ticks, constructParametersAdaptive);
            }
            else
            {
                output = generateHTML();
            }

            System.Diagnostics.Process.Start(output);
        }

        private void datasetWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            groupBox.Enabled = true;
            progressBar.Visible = false;
            progressBar.Value = 0;
        }
    }
}
