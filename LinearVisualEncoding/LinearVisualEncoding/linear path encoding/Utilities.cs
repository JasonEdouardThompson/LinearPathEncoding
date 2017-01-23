using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{

    public static class Helper
    {
        public static Color gradient(float[] values, Color[] colours, float value)
        {
            if (values.Length != colours.Length)
            {
                throw new ArgumentException("The values and colours arrays have different lengths.");
            }

            if (value <= values[0])
            {
                return colours[0];
            }
            int n = values.Length;
            if (value >= values[n - 1])
            {
                return colours[n - 1];
            }

            for (int i = 1; i < n; i++)
            {
                if (values[i] > value)
                {
                    float t = (value - values[i - 1]) / (values[i] - values[i - 1]);

                    Color cA = colours[i - 1], cB = colours[i];

                    return Color.FromArgb(
                        (int)(t * (cB.R - cA.R) + cA.R),
                        (int)(t * (cB.G - cA.G) + cA.G),
                        (int)(t * (cB.B - cA.B) + cA.B));
                }
            }
            return Color.HotPink;
        }

        public static float map(float v, float v0, float v1, float x0, float x1)
        {
            if (v < v0)
            {
                return x0;
            }

            if (v > v1)
            {
                return x1;
            }

            return (v - v0) / (v1 - v0) * (x1 - x0) + x0;
        }

        public static PointF moveAlongByD(PointF a, PointF b, float r, float limitPc)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;
            float d = (float)Math.Sqrt(dx * dx + dy * dy);

            r = r > d * limitPc ? d * limitPc : r;

            return new PointF(r * dx / d + a.X, r * dy / d + a.Y);
        }
    }

    public class KeyHelper
    {
        public static Font keyFont = new Font(SystemFonts.CaptionFont.FontFamily, SystemFonts.CaptionFont.Size );

        public static Bitmap generateCategoryKeyBitmap(string[] categories, Color[] colours, int boxSize, int margin, int s = 3)
        {
            int height = categories.Length * (boxSize + margin) + margin;
            Font font = keyFont;

            Bitmap b = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(b);
            int width = (int)categories.Select(c => g.MeasureString(c, font).Width).Max() + 2 * margin + boxSize;

            g.Dispose();
            b.Dispose();

            b = new Bitmap(width * s, height * s);
            g = Graphics.FromImage(b);
            g.ScaleTransform(s, s);
            g.Clear(Color.White);

            for (int i = 0; i < categories.Length; i++)
            {
                using (Brush br = new SolidBrush(colours[i]))
                {
                    float y = margin + i * (margin + boxSize);
                    g.FillRectangle(br, 0, y, boxSize, boxSize);
                    g.DrawString(categories[i], font, br, margin + boxSize, y - margin);
                }
            }
            return b;
        }

        public static Bitmap generateColourKeyBitmap(float[] values, Color[] colours, int boxWidth, int boxHeight, int margin, string title, int s = 3)
        {
            Font font = keyFont;

            Bitmap b = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(b);

            int charHeight = (int)g.MeasureString("A", font).Height;

            int width = (int)values.Select(c => g.MeasureString(string.Format("{0:0.0}", c), font).Width).Max() + 4 * margin + boxWidth + charHeight;
            int height = (int)(margin + boxHeight + charHeight + margin);

            g.Dispose();
            b.Dispose();

            b = new Bitmap(width * s, height * s);
            g = Graphics.FromImage(b);
            g.ScaleTransform(s, s);
            g.Clear(Color.White);

            float maxValue = values[values.Length - 1];
            float minValue = values[0];
            float y0 = 0;
            for (int i = 0; i < values.Length; i++)
            {
                float v = values[i];

                float y = Helper.map((float)i / (values.Length - 1), 0, 1, margin + boxHeight + charHeight / 2, margin + charHeight / 2);
                if (i > 0)
                {
                    using (Brush br = new System.Drawing.Drawing2D.LinearGradientBrush(new PointF(0, y0), new PointF(0, y), colours[i - 1], colours[i]))
                    {
                        g.FillRectangle(br, 0, y, boxWidth, Math.Abs(y - y0));
                    }
                }

                g.DrawLine(Pens.Black, boxWidth, y, (3 * margin) / 2 + boxWidth, y);
                g.DrawString(string.Format("{0:0.0}", v), font, Brushes.Black, margin + boxWidth, y - charHeight / 2);
                y0 = y;
            }
            g.DrawRectangle(Pens.Black, 0, margin + charHeight / 2, boxWidth, boxHeight);

            g.RotateTransform(90, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.DrawString(title, font, Brushes.Black, 0.5f * (height - g.MeasureString(title, font).Width), -(width));

            return b;
        }

        public static Bitmap generateThicknessKeyBitmapExtremes(
            float minValue, float maxValue,
            float startValue, float stopValue,
            float minThickness, float maxThickness,
            int boxHeight,
            int margin,
            int noTicks,
            string title,
            int s = 3)
        {
            Font font = keyFont;

            Bitmap b = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(b);

            int charHeight = (int)g.MeasureString("A", font).Height;

            int width = (int)(g.MeasureString(string.Format("{0:0.0}", maxValue), font).Width + 3 * margin + maxThickness + charHeight);
            int height = (int)(margin + boxHeight + charHeight + margin);

            g.Dispose();
            b.Dispose();

            b = new Bitmap(width * s, height * s);
            g = Graphics.FromImage(b);
            g.ScaleTransform(s, s);
            g.Clear(Color.White);

            float yt = margin + charHeight / 2;

            for (int i = 0; i < noTicks; i++)
            {
                float p = (float)i / (noTicks - 1);
                float v = Helper.map(p, 0, 1, minValue, maxValue);
                float w = Helper.map(p, 0, 1, minThickness, maxThickness);

                float y = Helper.map(p, 0, 1, yt + boxHeight, yt);
                g.DrawLine(Pens.Black, 0, y, maxThickness + margin / 2, y);
                g.DrawString(string.Format("{0:0.0}", v), font, Brushes.Black, maxThickness + margin / 2, y - charHeight / 2);

            }

            g.FillPolygon(Brushes.Black, new PointF[]{
                new PointF(0,yt), new PointF(maxThickness,yt),//top bit
                new PointF(maxThickness,Helper.map(stopValue, minValue, maxValue, yt + boxHeight, yt)),
                new PointF(maxThickness,Helper.map(stopValue, minValue, maxValue, yt + boxHeight, yt)),
                new PointF(maxThickness,Helper.map(stopValue, minValue, maxValue, yt + boxHeight, yt)),
                new PointF(minThickness,Helper.map(startValue, minValue, maxValue, yt + boxHeight, yt)),
                new PointF(minThickness,yt + boxHeight),
                new PointF(0,yt + boxHeight)
            });

            g.RotateTransform(90, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.DrawString(title, font, Brushes.Black, 0.5f * (height - g.MeasureString(title, font).Width), -(width));

            return b;
        }

        public static Bitmap generateThicknessKeyBitmap(float minValue, float maxValue, float minThickness, float maxThickness, int boxHeight, int margin, int noTicks, string title, int s = 3)
        {
            Font font = keyFont;

            Bitmap b = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(b);

            int charHeight = (int)g.MeasureString("A", font).Height;

            int width = (int)(g.MeasureString(string.Format("{0:0.0}", maxValue), font).Width + 3 * margin + maxThickness + charHeight);
            int height = (int)(margin + boxHeight + charHeight + margin);

            g.Dispose();
            b.Dispose();

            b = new Bitmap(width * s, height * s);
            g = Graphics.FromImage(b);
            g.ScaleTransform(s, s);
            g.Clear(Color.White);

            float yt = margin + charHeight / 2;

            for (int i = 0; i < noTicks; i++)
            {
                float p = (float)i / (noTicks - 1);
                float v = Helper.map(p, 0, 1, minValue, maxValue);
                float w = Helper.map(p, 0, 1, minThickness, maxThickness);

                float y = Helper.map(p, 0, 1, yt + boxHeight, yt);
                g.DrawLine(Pens.Black, 0, y, maxThickness + margin / 2, y);
                g.DrawString(string.Format("{0:0.0}", v), font, Brushes.Black, maxThickness + margin / 2, y - charHeight / 2);

            }

            g.FillPolygon(Brushes.Black, new PointF[]{
                new PointF(0,yt),
                new PointF(maxThickness,yt),
                new PointF(minThickness,yt + boxHeight),
                new PointF(0,yt + boxHeight)
            });

            g.RotateTransform(90, System.Drawing.Drawing2D.MatrixOrder.Append);
            g.DrawString(title, font, Brushes.Black, 0.5f * (height - g.MeasureString(title, font).Width), -(width));

            return b;
        }
    }
}
