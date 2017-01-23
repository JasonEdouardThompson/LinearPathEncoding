using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{

    public delegate Color ColourSelection<T>(T obj);
    public delegate float ThicknessSelection<T>(T obj);
    public delegate void EndMarkerDrawer(Graphics g, PointF focusPoint, PointF connectionPoint, TransferPath tp, Color? colorOverride = null);

    public abstract class Path
    {
        public int zorder;
        public Color colour;
        public float thickness;
        public abstract void draw(Graphics g, Color? colourOverride = null);
        public abstract RectangleF bounds { get; }
        public abstract float length { get; }
    }

    public class EntityDepictionParameters
    {
        public ColourSelection<Entity> colourSelection;
        public ThicknessSelection<Entity> thicknessSelection;
    }

    public class EntityPath : Path
    {
        public Entity entity;
        public DepictionInterval di;
        public float u, y, x0, x1;

        public override void draw(Graphics g, Color? colourOverride = null)
        {
            Color c = (colourOverride.HasValue ? colourOverride.Value : this.colour);
            using (Pen p = new Pen(c, this.thickness))
            {
                g.DrawLine(p, x0, y, x1, y);
            }
            //g.DrawString(entity.id + "", LinearVisualEncoding.keyFont, Brushes.Black, x0, y - 5);
        }

        public override RectangleF bounds
        {
            get
            {
                float s = 2 * Math.Max(1, thickness);
                return new RectangleF(x0 - s, y - s, x1 - x0 + 2 * s, 2 * s);
            }
        }

        public override float length
        {
            get { return x1 - x0; }
        }
    }

    public class TransferDepictionParameters
    {
        public EndMarkerDrawer startMarkerDrawer;
        public EndMarkerDrawer finishMarkerDrawer;

        public ColourSelection<Transfer> colourSelection;
        public ThicknessSelection<Transfer> thicknessSelection;

        public float ls, lf;
    }

    public class TransferPath : Path
    {
        public Transfer transfer;
        public PointF focusPointA, focusPointB;
        public PointF connectionPointA, connectionPointB;
        public EndMarkerDrawer startMarkerDrawer;
        public EndMarkerDrawer finishMarkerDrawer;

        public override void draw(Graphics g, Color? colourOverride = null)
        {
            Color c = colourOverride.HasValue ? colourOverride.Value : this.colour;

            using (Pen p = new Pen(c, this.thickness))
            {
                g.DrawLine(p, connectionPointA, connectionPointB);
                startMarkerDrawer(g, focusPointA, connectionPointA, this, c);
                finishMarkerDrawer(g, focusPointB, connectionPointB, this, c);
            }
        }

        public static float min(params float[] values)
        {
            float minV = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                minV = minV < values[i] ? minV : values[i];
            }
            return minV;
        }

        public static float max(params float[] values)
        {
            float minV = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                minV = minV > values[i] ? minV : values[i];
            }
            return minV;
        }
        public override RectangleF bounds
        {
            get
            {
                float x0 = min(focusPointA.X, focusPointB.X, connectionPointA.X, connectionPointB.X);
                float x1 = max(focusPointA.X, focusPointB.X, connectionPointA.X, connectionPointB.X);
                float y0 = min(focusPointA.Y, focusPointB.Y, connectionPointA.Y, connectionPointB.Y);
                float y1 = max(focusPointA.Y, focusPointB.Y, connectionPointA.Y, connectionPointB.Y);
                float s = 2 * thickness;
                return new RectangleF(x0 - s, y0 - s, x1 - x0 + 2 * s, y1 - y0 + 2 * s);
            }
        }

        public override float length
        {
            get { return Math.Abs(focusPointB.Y - focusPointA.Y); }
        }
    }
}
