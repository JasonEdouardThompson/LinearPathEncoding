using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{
    interface IRectanglePlottingTransform
    {
        PointF transform(DateTime t, float u);
        float transformX(DateTime t);
        float transformY(float u);
    }
    public class LinearRectanglePlottingTransform : IRectanglePlottingTransform
    {
        public DateTime t0, t1;
        public TimeSpan d;
        public float u0, u1;

        public float x0, y0, x1, y1;

        public LinearRectanglePlottingTransform(DateTime t0, DateTime t1, float u0, float u1, float x0, float y0, float x1, float y1)
        {
            this.t0 = t0;
            this.t1 = t1;
            this.d = t1 - t0;

            this.u0 = u0;
            this.u1 = u1;

            this.x0 = x0;
            this.y0 = y0;

            this.x1 = x1;
            this.y1 = y1;
        }

        public virtual PointF transform(DateTime t, float u)
        {
            return new PointF(transformX(t), transformY(u));
        }
        public virtual float transformX(DateTime t)
        {
            float p = (float)((t - t0).TotalMilliseconds / d.TotalMilliseconds);
            return Helper.map(p, 0, 1, x0, x1);
        }
        public virtual float transformY(float u)
        {
            return Helper.map(u, u0, u1, y0, y1);
        }
    }
    public class AdaptivePlottingTransform : LinearRectanglePlottingTransform
    {

        //public LinearRectanglePlottingTransform Sr;
        private float[] startingPoints;
        private float[] widths;
        private float tw;
        private int intervals;
        private int[] counts;
        private float w;


        public AdaptivePlottingTransform(int intervals, TransferGraph tg, DateTime t0, DateTime t1, float u0, float u1, float x0, float y0, float x1, float y1)
            : base(t0, t1, u0, u1, x0, y0, x1, y1)
        {
            //Sr = new LinearRectanglePlottingTransform(t0, t1, u0, u1, x0, y0, x1, y1);
            this.intervals = intervals;

            w = (x1 - x0);
            tw = w / intervals;

            counts = new int[intervals];

            foreach (var r in tg.transfers)
            {
                int i_s = (int)Math.Min(Math.Floor((base.transformX(r.ts) - x0) / tw), intervals - 1);
                int i_f = (int)Math.Min(Math.Floor((base.transformX(r.tf) - x0) / tw), intervals - 1);

                counts[i_s] += 1;
                counts[i_f] += 1;
            }

            float c = w / (counts.Sum() + intervals);

            widths = new float[intervals];

            for (int i = 0; i < intervals; i++)
            {
                widths[i] = (1 + counts[i]) * c;
            }

            startingPoints = new float[intervals];
            startingPoints[0] = x0;

            for (int i = 1; i < intervals; i++)
            {
                startingPoints[i] = widths[i - 1] + startingPoints[i - 1];
            }
        }

        public override float transformX(DateTime t)
        {
            float p = (base.transformX(t) - x0) / tw;
            int i = (int)Math.Min(Math.Floor(p), intervals - 1);
            return (p - i) * this.widths[i] + this.startingPoints[i];
        }

        public static string arrayToString<T>(T[] el)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < el.Length; i++)
            {
                sb.Append(el[i]);
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public override string ToString()
        {
            return String.Format("[widths:[{0}], starts:[{1}], counts:[{2}], percents[{3}]]",
                arrayToString(widths),
                arrayToString(startingPoints),
                arrayToString(counts),
                arrayToString<float>(widths.Select(w => w / this.w).ToArray()));
        }
    }

}
