using System;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LinearVisualEncoding
{
    public interface IDepictionIntervalStrategy
    {
        DepictionInterval compute(Entity e, Transfer[] transfers);
        String Description { get; }
    }

    public class EfficientDepictionIntervalStrategy : IDepictionIntervalStrategy
    {
        private TimeSpan minDuration;
        string IDepictionIntervalStrategy.Description { get { return "Efficient"; } }

        public EfficientDepictionIntervalStrategy(TimeSpan minDuration)
        {
            this.minDuration = minDuration;
        }

        public DepictionInterval compute(Entity e, Transfer[] transfers)
        {
            DepictionInterval di = new DepictionInterval();
            di.ts = e.outgoing.Select(r => r.ts).Concat(e.incoming.Select(r => r.tf)).Min();
            di.tf = e.outgoing.Select(r => r.ts).Concat(e.incoming.Select(r => r.tf)).Max();

            if ((di.tf - di.ts) < minDuration)
            {
                di.tf = di.ts + minDuration;
            }

            return di;
        }
    }
    public class UniformDepictionIntervalStrategy : IDepictionIntervalStrategy
    {
        private DepictionInterval di;

        string IDepictionIntervalStrategy.Description { get { return "Uniform"; } }

        public DepictionInterval compute(Entity e, Transfer[] transfers)
        {
            di = new DepictionInterval();
            di.ts = transfers.Select(r => r.ts).Min();
            di.tf = transfers.Select(r => r.tf).Max();
            return di;
        }

    }
    public class DiagramParameters
    {
        public int noTickMarks;
        public Color lineColour; 
        public string labelString;
        public float labelMargin;
        public Font font;
    }
    public class LinearPathEncoding
    {
        public static readonly Font tickFont = new Font(SystemFonts.CaptionFont.FontFamily, (SystemFonts.CaptionFont.Size * 3) / 2);

        public static Path[] constructPaths(
            TransferGraph tg, 
            LinearRectanglePlottingTransform S,
            Arrangement q,
            EntityDepictionParameters edp,
            TransferDepictionParameters tdp)
        {
            Path[] paths = new Path[tg.entities.Length + tg.transfers.Length];

            var entityPaths = constructEntityPaths(tg, S, q, edp);
            var transferPaths = constructTransferPaths(tg, S, entityPaths, tdp);

            Array.Copy(entityPaths, 0, paths, 0, entityPaths.Length);
            Array.Copy(transferPaths, 0, paths, entityPaths.Length, transferPaths.Length);

            return paths.OrderBy(p => -p.thickness * p.length).ToArray();
            //return paths.OrderBy(p => -p.length).ToArray();
        }

        public static EntityPath[] constructEntityPaths(
            TransferGraph tg, 
            LinearRectanglePlottingTransform S,
            Arrangement q,
            EntityDepictionParameters edp)
        {
            EntityPath[] paths = new EntityPath[ tg.entities.Length ];
            
            float N = q.N;

            for (int i = 0; i < tg.entities.Length; i++)
			{
                Entity e = tg.entities[i];

                EntityPath ep = paths[i] = new EntityPath();                

                ep.entity = e;
                ep.di = e.di;
                ep.colour = edp.colourSelection(e);
                ep.thickness = edp.thicknessSelection(e);
                ep.u = q[e] / (N + 1) * (S.u1 - S.u0 ) + S.u0;
                ep.y = S.transformY(ep.u);
                ep.x0 = S.transformX( ep.di.ts );
                ep.x1 = S.transformX( ep.di.tf );
			}
            return paths;
        }

        public static TransferPath[] constructTransferPaths(
            TransferGraph tg, 
            LinearRectanglePlottingTransform S, 
            EntityPath[] entityPaths,
            TransferDepictionParameters tdp)
        {
            TransferPath[] paths = new TransferPath[tg.transfers.Length];

            for (int i = 0; i < tg.transfers.Length; i++)
            {
                Transfer r = tg.transfers[i];
                TransferPath rp = paths[i] = new TransferPath();

                EntityPath srcPath = entityPaths.First(e => e.entity.id == r.source.id );
                EntityPath dstPath = entityPaths.First(e => e.entity.id == r.destination.id );
                float d = 0.5f * Math.Sign(dstPath.y - srcPath.y);
                rp.transfer = r;
                if (d != 0)
                {
                    rp.focusPointA = new PointF(S.transformX(r.ts), srcPath.y + d * srcPath.thickness);
                    rp.focusPointB = new PointF(S.transformX(r.tf), dstPath.y - d * dstPath.thickness);
                    rp.connectionPointA = Helper.moveAlongByD(rp.focusPointA, rp.focusPointB, tdp.ls, limitPc: 0.4f);
                    rp.connectionPointB = Helper.moveAlongByD(rp.focusPointB, rp.focusPointA, tdp.lf, limitPc: 0.4f);
                }
                else
                {
                    rp.focusPointA = new PointF(S.transformX(r.ts), srcPath.y + 0.5f * srcPath.thickness);
                    rp.focusPointB = new PointF(S.transformX(r.tf), dstPath.y + 0.5f * dstPath.thickness);
                    rp.connectionPointA = new PointF(rp.focusPointA.X, rp.focusPointA.Y + tdp.ls);
                    rp.connectionPointB = rp.connectionPointA;
                }

                rp.colour = tdp.colourSelection(r);
                rp.thickness = tdp.thicknessSelection(r);
                rp.startMarkerDrawer = tdp.startMarkerDrawer;
                rp.finishMarkerDrawer = tdp.finishMarkerDrawer;

            }
            return paths;
        }

        public static void drawTickMarks(Graphics g, LinearRectanglePlottingTransform S, DiagramParameters dp)
        {
            Font font = LinearPathEncoding.tickFont;
            using (Pen p = new Pen(dp.lineColour))
            {
                float xCutoff = S.x0;

                for (int i = 0; i < dp.noTickMarks; i++)
                {                    
                    float u = (float)i / (dp.noTickMarks - 1);

                    DateTime t = S.t0.AddMilliseconds(u * S.d.TotalMilliseconds);

                    g.DrawLine(p, S.transform(t, -0.1f), S.transform(t, 1));

                    string label = t.ToString(dp.labelString);
                    float x = S.transformX(t);
                    if (x < xCutoff)
                    {
                        continue;
                    }
                    float w = g.MeasureString(label, font).Width;
                    if (x + w > g.VisibleClipBounds.Right && x - w > xCutoff)
                    {
                        g.DrawString(label, font, Brushes.Black, new PointF(x-w, 0));
                    }
                    else
                    {
                        g.DrawString(label, font, Brushes.Black, new PointF(x, 0));
                    }
                    xCutoff = x + w + dp.labelMargin;
                }
            }
        }
     
        public static void draw(
            Graphics g,
            Arrangement q,
            TransferGraph tg,
            EntityDepictionParameters edp, 
            TransferDepictionParameters tdp, 
            LinearRectanglePlottingTransform S, 
            DiagramParameters dp)
        {
            var paths = constructPaths(tg, S, q, edp, tdp);

            drawTickMarks(g, S, dp);

            foreach (var p in paths)
            {
                p.draw(g);
            }
        }
    }
}
