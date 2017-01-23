using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{
    public class AirlineEntityAttributes : IBackgroundable
    {
        public string name;
        public bool isBackgrounded { get; set; }

        public AirlineEntityAttributes(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return string.Format("[name: {0}]", name);
        }
    }
    public class AirlineTransferAttributes : IBackgroundable
    {

        public float price;
        public string airline;
        public bool isBackgrounded { get; set; }
        
        public AirlineTransferAttributes(float price, string airline)
        {
            this.price = price;
            this.airline = airline;
        }

        public override string ToString()
        {
            return string.Format("[price: {0}, airline: {1}]", price, airline);
        }
    }
    public class AirlineTransferGraph
    {
        public static TransferGraph LoadTransferGraph(
            string filename,
            DepictionInterval.DepictionIntervalSelection dis)
        {
            Dictionary<string, Entity> entities = new Dictionary<string, Entity>();
            List<Transfer> transfers = new List<Transfer>();
            //load the transfer

            using (System.IO.StreamReader file = new System.IO.StreamReader(filename))
            {
                //ignore the header we assume that it is
                //"source", "destination", "startingTime", "finishingTime", "score", "size", "message", "thread"
                file.ReadLine();

                int id = 0;
                int transferId = 0;

                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var chunks = CommentTransferGraph.splitCSVLine(line);
                    if (chunks.Length == 0)
                    {
                        continue;
                    }
                    string source = chunks[0];
                    if (!entities.ContainsKey(source))
                    {
                        entities[source] = new Entity(
                            id++,
                            source,
                            new AirlineEntityAttributes(source));
                    }
                    string destination = chunks[1];
                    if (!entities.ContainsKey(destination))
                    {
                        entities[destination] = new Entity(
                            id++,
                            destination,
                            new AirlineEntityAttributes(destination));
                    }

                    Transfer r = new Transfer(
                        id: transferId++,
                        source: entities[source],
                        destination: entities[destination],
                        ts: DateTime.ParseExact(chunks[2], "dd/MM/yyyy H:mm", System.Globalization.CultureInfo.InvariantCulture),
                        tf: DateTime.ParseExact(chunks[3], "dd/MM/yyyy H:mm", System.Globalization.CultureInfo.InvariantCulture),
                        attributes: new AirlineTransferAttributes(
                            price: float.Parse(chunks[4]),
                            airline: chunks[5]));

                    transfers.Add(r);
                }
            }

            Transfer[] transfersArr = transfers.ToArray();
            Entity[] entitiesArr = entities.Values.OrderBy(el => el.id).ToArray();

            for (int i = 0; i < entitiesArr.Length; i++)
            {
                Entity e = entitiesArr[i];
                e.di = dis(e, transfersArr);
            }

            TransferGraph g = new TransferGraph(entitiesArr, transfersArr);

            return g;
        }
    }
}
