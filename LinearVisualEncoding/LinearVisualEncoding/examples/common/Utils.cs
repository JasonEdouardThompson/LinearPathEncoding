using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{

    public class SVGHelper
    {
        private StringBuilder sb = new StringBuilder();

        public SVGHelper drawTag(string tagText)
        {
            sb.Append(tagText);
            return this;
        }

        public SVGHelper DrawVerticalLine(double x, double y0, double y1, string strokeColour, double strokeWidth)
        {
            sb.AppendFormat($"<line x1=\"{x}\" y1=\"{y0}\" x2=\"{x}\" y2=\"{y1}\" style=\"stroke:{strokeColour}; stroke-width:{strokeWidth}\" />");
            return this;
        }

        public SVGHelper DrawLine(double x0, double y0, double x1, double y1, string strokeColour, double strokeWidth)
        {
            sb.AppendFormat($"<line x1=\"{x0}\" y1=\"{y0}\" x2=\"{x1}\" y2=\"{y1}\" style=\"stroke:{strokeColour}; stroke-width:{strokeWidth}\" />");
            return this;
        }
        public SVGHelper DrawText(double x, double y, string text, string fill)
        {
            sb.AppendFormat($"<text x=\"{x}\" y=\"{y}\" fill=\"{fill}\">{text}</text>");
            return this;
        }

        public override string ToString()
        {
            return sb.ToString();
        }


        public SVGHelper DrawArrowHead(double x0, double y0, double x1, double y1, double width, double height, string colour)
        {
            sb.Append(orientToEndLine(x0, y0, x1, y1, getArrowHead(width, height, colour)));
            return this;
        }

        public SVGHelper startGroup(Dictionary<String,String> attributes )
        {
            sb.Append("<g");
            foreach (var kvp in attributes)
            {
                sb.Append($" {kvp.Key}=\"{kvp.Value}\"");
            }
            sb.Append(">");

            return this;
        }

        public SVGHelper endGroup()
        {
            sb.Append("</g>");
            return this;
        }
        

        private static string orientToEndLine(double x0, double y0, double x1, double y1, string tag)
        {
            double angle = Math.Atan2(x1 - x0, y1 - y0) * 180.0 / Math.PI;
            return $"<g transform=\"translate({x1},{y1}) rotate({180 - angle})\"> {tag} </g>";

        }
        private static string getArrowHead(double width, double height, string colour)
        {
            return $"<polygon points=\"0,0,{0.5 * width}, {height}, {-0.5 * width}, {height}\" style=\"fill:{colour}\"/>";

        }
    }

    public enum HeightType
    {
        RowHeight,
        DiagramHeight
    }
    public interface IBackgroundable
    {
        bool isBackgrounded { get; set; }

    }
    class Utils
    {

        //http://www.fluxbytes.com/csharp/convert-datetime-to-unix-time-in-c/
        public static long ConvertToUnixTimeMilliSeconds(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalMilliseconds;
        }
        public static bool isBackgrounded(Path p)
        {
            return (p is EntityPath && ((p as EntityPath).entity.attributes as IBackgroundable).isBackgrounded)
                || (p is TransferPath && ((p as TransferPath).transfer.attributes as IBackgroundable).isBackgrounded);
        }

        public static void backgrounGroup(TransferGraph tg, IEnumerable<Transfer>[] groupsArray, int i)
        {
            var group = groupsArray[i];

            foreach (var r in group)
            {
                (r.attributes as IBackgroundable).isBackgrounded = true;
            }

            foreach (var e in tg.entities)
            {
                (e.attributes as IBackgroundable).isBackgrounded = true;
                foreach (var r in e.transfers)
                {
                    if (!(r.attributes as IBackgroundable).isBackgrounded)
                    {
                        (e.attributes as IBackgroundable).isBackgrounded = false;
                        break;
                    }
                }
            }
        }

    }
}
