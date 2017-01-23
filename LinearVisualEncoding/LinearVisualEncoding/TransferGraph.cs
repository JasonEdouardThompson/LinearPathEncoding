using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{
    public class DepictionInterval
    {
        public delegate DepictionInterval DepictionIntervalSelection(Entity e, Transfer[] tArr );
        public DateTime ts, tf;
    }

    public class Entity
    {

        public int id;
        public string key;

        public DepictionInterval di;

        public object attributes;

        public List<Transfer> transfers = new List<Transfer>();
        public List<Transfer> incoming = new List<Transfer>();
        public List<Transfer> outgoing = new List<Transfer>();

        public Entity(int id, string key, object attributes)
        {
            this.id = id;
            this.key = key;
            this.attributes = attributes;
        }
    }

    public class Transfer
    {
        public int id;
        public Entity source, destination;
        public DateTime ts, tf;

        public object attributes;

        public Transfer(int id, Entity source, Entity destination, DateTime ts, DateTime tf, object attributes)
        {
            this.id = id;
            this.source = source;
            this.destination = destination;
            this.ts = ts;
            this.tf = tf;
            this.attributes = attributes;

            source.outgoing.Add(this);
            source.transfers.Add(this);

            destination.incoming.Add(this);
            destination.transfers.Add(this);
        }
    }

    public class TransferGraph
    {
        public Entity[] entities;
        public Transfer[] transfers;
        public DateTime t0, t1;
        
        //http://stackoverflow.com/questions/11364481/datetime-round-up-and-down
        public static DateTime roundDown(DateTime dateTime, TimeSpan interval)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % interval.Ticks));
        }
        public static DateTime roundUp(DateTime dateTime, TimeSpan interval)
        {
            var overflow = dateTime.Ticks % interval.Ticks;

            return overflow == 0 ? dateTime : dateTime.AddTicks(interval.Ticks - overflow);
        }

        public TransferGraph(Entity[] entities, Transfer[] transfers)
        {
            this.entities = entities;
            this.transfers = transfers;

            this.t0 = transfers.Select(t => t.ts).Concat(entities.Select(e =>e.di.ts)).Min();
            this.t1 = transfers.Select(t => t.tf).Concat(entities.Select(e =>e.di.tf)).Max();

            this.t0 = roundDown(this.t0, new TimeSpan(0, 30, 0));
            this.t1 = roundUp(this.t1, new TimeSpan(0, 30, 0));
        }
    }
}
