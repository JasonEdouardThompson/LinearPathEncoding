using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{
    public class CommentEntityVisualAppearance
    {
        public Color generatorColour, provokerColour;

        //size of the transfer
        public float minExpectedScore, maxExpectedScore;
        public float minThickness, maxThickness;

        public CommentEntityVisualAppearance(TransferGraph g)
        {
            generatorColour = Color.FromArgb(0x19, 0x76, 0xD2);
            provokerColour = Color.FromArgb(0xE6, 0x4A, 0x19);

            var attributes = g.entities.Select(e => (e.attributes as CommentEntityAttributes));

            minExpectedScore = attributes.Select(r => r.expectedScore).Min();
            maxExpectedScore = attributes.Select(r => r.expectedScore).Max();

            minThickness = 1.0f;
            maxThickness = 7.0f;
        }

        public Color colourSelection(Entity e)
        {
            return (e.attributes as CommentEntityAttributes).role == "generator" ? generatorColour : provokerColour;
        }

        public float thicknessSelection(Entity e)
        {
            return Helper.map(
                (e.attributes as CommentEntityAttributes).expectedScore,
                minExpectedScore, maxExpectedScore,
                minThickness, maxThickness);
        }
    }
    public class CommentTransferVisualAppearance
    {
        //score of the transfer
        public float[] values;
        public Color[] colours;

        //size of the transfer
        public float minSize, maxSize;
        public float minThickness, maxThickness;

        public CommentTransferVisualAppearance(TransferGraph g)
        {
            var attributes = g.transfers.Select(r => (r.attributes as CommentTransferAttributes));
            var scores = attributes.OrderBy(t => t.score).ToArray();

            int lp = (int)Math.Floor(0.1f * scores.Length), hp = (int)Math.Floor(0.9f * scores.Length);

            List<float> valuesL = new List<float>(6);
            List<Color> coloursL = new List<Color>(6);

            //three cases
            //all positive

            Color zeroColor = Color.FromArgb(50, 50, 50);
            if (scores[0].score >= 0)
            {
                valuesL.Add(0); coloursL.Add(zeroColor);
                if (scores[hp].score != 0)
                {
                    valuesL.Add(scores[hp].score); coloursL.Add(Color.Green);
                }
                valuesL.Add(scores[scores.Length - 1].score); coloursL.Add(Color.Green);
            }
            else if (scores[scores.Length - 1].score < 0)//all negative
            {
                valuesL.Add(scores[0].score); coloursL.Add(Color.Red);

                if (scores[lp].score != 0)
                {
                    valuesL.Add(scores[lp].score); coloursL.Add(Color.Red);

                }
                valuesL.Add(scores[scores.Length - 1].score); coloursL.Add(zeroColor);
            }
            else//positive and negative
            {
                valuesL.Add(scores[0].score); coloursL.Add(Color.Red);

                if (scores[lp].score != 0)
                {
                    valuesL.Add(scores[lp].score); coloursL.Add(Color.Red);

                }
                valuesL.Add(0); coloursL.Add(zeroColor);

                if (scores[hp].score != 0)
                {
                    valuesL.Add(scores[hp].score); coloursL.Add(Color.Green);
                }
                valuesL.Add(scores[scores.Length - 1].score); coloursL.Add(Color.Green);
            }

            values = valuesL.ToArray();
            colours = coloursL.ToArray();

            minSize = attributes.Select(r => r.size).Min();
            maxSize = attributes.Select(r => r.size).Max();

            minThickness = 1.5f;
            maxThickness = 7.0f;
        }
        public Color colourSelection(Transfer t)
        {
            return Helper.gradient(
                values,
                colours,
                (t.attributes as CommentTransferAttributes).score);
        }
        public float thicknessSelection(Transfer t)
        {
            return Helper.map(
                (t.attributes as CommentTransferAttributes).size,
                minSize, maxSize,
                minThickness, maxThickness);
        }
    }
    public class CommentEndMarkerStrategy
    {
        public float ls, lf, ds, df, minS;

        public CommentEndMarkerStrategy(float ls, float lf, float ds, float df, float minS)
        {
            this.ls = ls;
            this.lf = lf;
            this.ds = ds;
            this.df = df;
            this.minS = minS;
        }

        public void drawFinishMarker(Graphics g, PointF focusPoint, PointF connectionPoint, TransferPath tp, Color? colorOverride)
        {
            float w = tp.thickness;
            float s = w + 2 * df;

            Color c = colorOverride.HasValue ? colorOverride.Value : tp.colour;

            drawArrowHead(g, focusPoint, connectionPoint, c, s);

        }

        public void drawStartMarker(Graphics g, PointF focusPoint, PointF connectionPoint, TransferPath tp, Color? colorOverride)
        {
            float w = tp.thickness;
            float s = Math.Min(Math.Max(w - 2 * ds, minS), w);

            Color c = colorOverride.HasValue ? colorOverride.Value : tp.colour;

            drawArrowHead(g, focusPoint, connectionPoint, c, s);
        }

        private static void drawArrowHead(Graphics g, PointF focusPoint, PointF connectionPoint, Color c, float width)
        {
            float dx = focusPoint.X - connectionPoint.X;
            float dy = focusPoint.Y - connectionPoint.Y;
            float d = (float)Math.Sqrt(dx * dx + dy * dy);

            //rotate 90 CW and normalise (dx,dy)
            float nx = -0.5f * dy / d;
            float ny = 0.5f * dx / d;
            using (Brush b = new SolidBrush(c))
            {
                g.FillPolygon(b, new PointF[]{
                    focusPoint,
                    new PointF(connectionPoint.X + width * nx, connectionPoint.Y + width * ny),
                    new PointF(connectionPoint.X - width * nx, connectionPoint.Y - width * ny)
                });
            }
        }
    }
    public class VisualParameters
    {
        public DiagramParameters dp;

        public LinearRectanglePlottingTransform S;

        public EntityDepictionParameters edp;
        public CommentEntityVisualAppearance eva;

        public TransferDepictionParameters tdp;
        public CommentTransferVisualAppearance tva;
        public CommentEndMarkerStrategy cems;
    }
}
