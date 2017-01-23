using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{
    public class AirlineEntityVisualAppearance
    {
        public Dictionary<string, Color> airportColours;
        public float thickness;

        public AirlineEntityVisualAppearance(TransferGraph tg, float thickness)
        {
            var _airportColours = new Dictionary<string, Color>()
            {
                { "Launceston", Color.FromArgb(0xa6, 0xce, 0xe3)},
                { "Canberra", Color.FromArgb(0x1f,78,0xb4)},
                { "Brisbane", Color.FromArgb(0xb2,0xdf,0x8a)},
                { "Alice Springs", Color.FromArgb(0x33,0xa0,0x2c)},
                { "Melbourne", Color.FromArgb(0xfb,0x9a,0x99)},
                { "Cairns", Color.FromArgb(0xe3,0x1a,0x1c)},
                { "Perth", Color.FromArgb(0xfd,0xbf,0x6f)},
                { "Sydney", Color.FromArgb(0xff,0x7f,0x00)},
                { "Hobart", Color.FromArgb(0xca,0xb2,0xd6)},
                { "Gold Coast", Color.FromArgb(0x6a,0x3d,0x9a)},
                { "Darwin", Color.FromArgb(0xff,0xff,0x99)},
                { "Adelaide", Color.FromArgb(0xb1,0x59,0x28)}
            };

            this.airportColours = new Dictionary<string, Color>();

            foreach (var e in tg.entities)
            {
                if (!airportColours.ContainsKey(e.key))
                {
                    airportColours[e.key] = _airportColours[e.key];
                }
            }

            this.thickness = thickness;
        }

        public Color colourSelection(Entity e)
        {
            return airportColours[e.key];
        }

        public float thicknessSelection(Entity e)
        {
            return thickness;
        }
    }
    public abstract class AirlineTransferVisualAppearance
    {
        //size of the transfer
        public float startPrice, stopPrice, minPrice, maxPrice;
        public float minThickness, maxThickness;

        protected AirlineEntityVisualAppearance aeva;

        public AirlineTransferVisualAppearance(TransferGraph g, AirlineEntityVisualAppearance aeva)
        {
            this.aeva = aeva;

            var prices = g.transfers
                .Select(r => (r.attributes as AirlineTransferAttributes))
                .OrderBy(t => t.price)
                .ToArray();

            int lp = (int)Math.Floor(0.1f * prices.Length), hp = (int)Math.Floor(0.9f * prices.Length);

            startPrice = prices[lp].price;
            stopPrice = prices[hp].price;

            minPrice = prices[0].price;
            maxPrice = prices[prices.Length - 1].price;

            minThickness = 1.0f;
            maxThickness = 5.0f;
        }

        public abstract Color colourSelection(Transfer t);

        public float thicknessSelection(Transfer t)
        {
            return Helper.map(
                (t.attributes as AirlineTransferAttributes).price,
                startPrice, stopPrice,
                minThickness, maxThickness);
        }

        public abstract string[] getKeys();
        public abstract Color[] getColors();
    }
    public class AirlineTransferVisualAppearanceByAirline : AirlineTransferVisualAppearance
    {

        public AirlineTransferVisualAppearanceByAirline(TransferGraph g, AirlineEntityVisualAppearance aeva) :
            base(g, aeva)
        {
        }

        private Color[] colours = new Color[] {
            Color.FromArgb(0x1f, 78, 0xb4),
            Color.FromArgb(0xe3, 0x1a, 0x1c),
            Color.FromArgb(0xff, 0x7f, 0x00)
        };

        private String[] keys = new String[] {
            "Airline A",
            "Airline B",
            "Airline C"
        };

        public override Color colourSelection(Transfer t)
        {
            AirlineTransferAttributes ata = t.attributes as AirlineTransferAttributes;

            if (ata.airline == "Airline A")
            {
                return Color.FromArgb(0x1f, 78, 0xb4);
            }
            else if (ata.airline == "Airline B")
            {
                return Color.FromArgb(0xe3, 0x1a, 0x1c);
            }
            else if (ata.airline == "Airline C")
            {
                return Color.FromArgb(0xff, 0x7f, 0x00);
            }
            return Color.FromArgb(0xff, 0x7f, 0x00);
        }

        public override Color[] getColors()
        {
            return colours;
        }

        public override string[] getKeys()
        {
            return keys;
        }
    }
    public class AirlineTransferVisualAppearanceBySource : AirlineTransferVisualAppearance
    {

        public AirlineTransferVisualAppearanceBySource(TransferGraph g, AirlineEntityVisualAppearance aeva) :
            base(g, aeva)
        {
        }
        public override Color colourSelection(Transfer t)
        {
            return this.aeva.colourSelection(t.source);
        }


        public override Color[] getColors()
        {
            return aeva.airportColours.Values.ToArray();
        }

        public override string[] getKeys()
        {
            return aeva.airportColours.Keys.ToArray();
        }
    }
    public class AirlineTransferVisualAppearanceByDestination : AirlineTransferVisualAppearance
    {

        public AirlineTransferVisualAppearanceByDestination(TransferGraph g, AirlineEntityVisualAppearance aeva) :
            base(g, aeva)
        {
        }
        public override Color colourSelection(Transfer t)
        {
            return this.aeva.colourSelection(t.destination);
        }

        public override Color[] getColors()
        {
            return aeva.airportColours.Values.ToArray();
        }

        public override string[] getKeys()
        {
            return aeva.airportColours.Keys.ToArray();
        }
    }
    public class ALVisualParameters
    {
        public DiagramParameters dp;

        public LinearRectanglePlottingTransform S;

        public EntityDepictionParameters edp;
        public AirlineEntityVisualAppearance eva;

        public TransferDepictionParameters tdp;
        public AirlineTransferVisualAppearance tva;
        public CommentEndMarkerStrategy cems;
    }
}
