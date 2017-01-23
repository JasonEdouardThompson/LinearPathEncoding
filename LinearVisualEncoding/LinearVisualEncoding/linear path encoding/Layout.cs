using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{
    public static class LayoutOptimisation
    {
        public delegate float[] CostFunction(Arrangement q);

        public static void InsertGreedy(Entity e, Arrangement q, CostFunction C)
        {
            float[] lowestCost = new float[]{float.MaxValue, float.MaxValue};
            int bestRow = 0;
            int n = q.N;
            for (int r = 1; r <= n + 1; ++r)
            {
                q.insert(e, r);
                float[] c = C(q);
                if ((c[0] < lowestCost[0] 
                    || (c[0] == lowestCost[0] && c[1] < lowestCost[1])) && !q.hasHorizontalTransfers())
                {
                    bestRow = r;
                    lowestCost = c;
                }
                q.remove(e);
            }
            q.insert(e, bestRow);
        }

        public static Arrangement ConstructGreedy(TransferGraph g, CostFunction C, Func<Entity,float> key)
        {
            Entity[] entities = g.entities.OrderBy(key).ToArray();

            Arrangement q = new Arrangement(g);

            foreach (Entity e in entities)
            {
                InsertGreedy(e, q, C);
            }

            return q;
        }

        public static void iterateGreedy(
            Arrangement q, 
            CostFunction C,
            IEnumerable<Entity> eitr)
        {
            //true if the entity has been re-inserted into the same spot
            bool[] checkList = new bool[q.tg.entities.Length];

            for (int i = 0; i < checkList.Length; i++)
            {
                checkList[i] = false;
            }

            int count = 0;
            foreach (Entity e in eitr)
            {
                if (count == checkList.Length)
                {
                    break;
                }

                //skip an entity if we have already reinserted it into the same place
                if (checkList[e.id])
                {
                    continue;
                }

                int initialRow = q[e];
                q.remove(e);
                InsertGreedy(e, q, C);
                int finalRow = q[e];

                if (initialRow != finalRow)
                {
                    System.Diagnostics.Debug.Write("*");
                    for (int i = 0; i < checkList.Length; i++)
                    {
                        checkList[i] = false;
                    }
                    count = 0;
                }

                checkList[e.id] = true;
                count += 1;
            }
            System.Diagnostics.Debug.WriteLine(count);
        }
        
        public static void shuffleArray<T>(T[] arr)
        { 
            Random random = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                T v = arr[i];
                int j = random.Next(i, arr.Length - 1);
                arr[i] = arr[j];
                arr[j] = v;
            }
        }
        public static IEnumerable<Entity> iterateRandomOrder( TransferGraph tg, int n)
        {
            Random random = new Random();
            Entity[] arr = (Entity[])tg.entities.Clone();
            int c = 0;
            while (c < n)
            {
                shuffleArray(arr);
                for (int i = 0; c < n && i < arr.Length; i++)
                {
                    c += 1;
                    yield return tg.entities[i];
                } 
            }

            yield break;
        }
    }

    public class Arrangement
    {
        int[] entities;
        public int N { get { return entities.Max(); } }
        public TransferGraph tg;

        public bool hasHorizontalTransfers()
        {
            for (int i = 0; i < tg.transfers.Length; i++)
            {
                Transfer r = tg.transfers[i];
                int s = entities[r.source.id];
                int f = entities[r.destination.id];

                if ( s > 0 && f > 0 && s == f && r.source.id != r.destination.id)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(entities.Length * 2);
            for (int i = 0; i < entities.Length; i++)
            {
                sb.Append(entities[i]);
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public void FromString( string s ){
            string[] chunks = s.Split(',');

            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = int.Parse(chunks[i]);
            }
        }
        //array of entities to check for each transfer
        int[][] checkArray;
        public Arrangement(Arrangement source){
            entities = (int[])source.entities.Clone();
            checkArray = source.checkArray;
            tg = source.tg;
        }

        public Arrangement(TransferGraph tg)
        {
            this.tg = tg;
            entities = new int[tg.entities.Length];
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = 0;
            }

            //implement the check array
            checkArray = new int[tg.transfers.Length][];
            
            List<int> indices = new List<int>();

            for (int i = 0; i < tg.transfers.Length; i++)
            {
                indices.Clear();
                var r = tg.transfers[i];

                for (int j = 0; j < entities.Length; j++)
                {
                    var e = tg.entities[j];

                    if (r.source == e
                        || r.destination == e
                        || r.ts < e.di.ts
                        || r.tf > e.di.tf)
                    {
                        continue;
                    }
                    indices.Add(j);
                }
                checkArray[i] = indices.ToArray();
            }
        }
        public int this[Entity e]
        {
            get { return entities[e.id]; }
        }

        public void shiftDown(int r)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] >= r)
                {
                    entities[i] += 1;
                }
            }
        }

        public void shiftUp(int r)
        {
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] >= r)
                {
                    entities[i] -= 1;
                }
            }
        }

        public void insert(Entity e, int r)
        {
            //check if there is space first
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] == r)
                {
                    var _e = tg.entities[i];
                    if (_e.di.tf < e.di.ts || _e.di.ts > e.di.tf)
                    {
                        continue;
                    }
                    shiftDown(r);
                    break;
                }
            }
            entities[e.id] = r;
        }

        public void remove(Entity e)
        {
            int r = entities[e.id];
            entities[e.id] = 0;

            //remove any empty spaces
            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] == r)
                {
                    return;
                }
            }
            shiftUp(r + 1);
        }

        public float[] costCombined()
        {
            return new float[] { costIntersectionER_instantaneousTransfers(), costLength() };
        }

        public float[] costCombinedLI()
        {
            return new float[] { costLength(), costIntersectionER_instantaneousTransfers() };
        }


        public float costLength()
        {
            float total = 0;
            for (int i = 0; i < tg.transfers.Length; i++)
            {
                Transfer r = tg.transfers[i];
                int s = entities[r.source.id];
                int f = entities[r.destination.id];

                if (s != 0 && f != 0)
                {
                    total += Math.Abs(s - f);
                }
            }
            return total;
        }

        public float costIntersectionER_instantaneousTransfers_Test()
        {
            float a = costIntersectionER_instantaneousTransfers();
            float b = costIntersectionER_instantaneousTransfers_BF();
            if (a != b)
            {
                throw new Exception();
            }

            return a;
        }
        public float costIntersectionER_instantaneousTransfers()
        {
            int total = 0;

            for (int i = 0; i < tg.transfers.Length; i++)
            {
                Transfer r = tg.transfers[i];
                int s = entities[r.source.id];
                int f = entities[r.destination.id];

                if (s == 0 || f == 0 || s == f)
                {
                    continue;
                }
                if (s > f)
                {
                    int v = s;
                    s = f;
                    f = v;
                }

                for (int j = 0; j < checkArray[i].Length; j++)
                {
                    int k = checkArray[i][j];

                    int re = entities[k];
                    Entity e = tg.entities[k];

                    if (re == 0
                        || re <= s
                        || re >= f)
                    {
                        continue;
                    }
                    total += 1;
                }
            }
            return total;
        }

        public float costIntersectionER_instantaneousTransfers_BF()
        {
            int total = 0;

            for (int i = 0; i < tg.transfers.Length; i++)
            {
                Transfer r = tg.transfers[i];
                int s = entities[r.source.id];
                int f = entities[r.destination.id];

                if (s == 0 || f == 0 || s == f )
                {
                    continue;
                }
                if (s > f )
                {
                    int v = s;
                    s = f;
                    f = v;
                }

                for (int j = 0; j < tg.entities.Length; j++)
                {
                    int re = entities[j];
                    
                    Entity e = tg.entities[j];

                    if (re == 0
                        || re <= s 
                        || re >= f
                        || r.ts < e.di.ts
                        || r.tf > e.di.tf )
                    {
                        continue;
                    }
                    total += 1;
                }
            }
            return total;
        }
    }
}
