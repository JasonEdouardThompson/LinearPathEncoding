using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearVisualEncoding
{
    public class CommentEntityAttributes : IBackgroundable
    {
        public string name;
        public string role;
        public float expectedScore;
        public bool isBackgrounded { get; set; }

        public CommentEntityAttributes(string name, string role, float expectedScore )
        {
            this.name = name;
            this.role = role;
            this.expectedScore = expectedScore;
        }
    }
    public class CommentTransferAttributes : IBackgroundable
    {

        public int score;
        public int size;
        public string message;
        public string thread;
        public string href;
        public string parentHref;
        public CommentTransferAttributes parent;
        public List<CommentTransferAttributes> children = new List<CommentTransferAttributes>();
        public Transfer transfer;

        public bool isBackgrounded { get; set; }

        public CommentTransferAttributes(
            int score,
            int size,
            string message,
            string thread,
            string href,
            string parentHref)
        {
            this.score = score;
            this.size = size;
            this.message = message;
            this.thread = thread;
            this.href = href;
            this.parentHref = parentHref;
        }
    }
    public static class CommentTransferGraph
    {
        public static string[] splitCSVLine( string line ){
            
            List<string> chunks = new List<string>();

            bool isInsideQuotes = false;
            int start = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if ( isInsideQuotes && line[i] == '"' && i <= line.Length - 2 && line[i + 1] == '"' )
                {
                    i += 1;//skip double double quotes
                }else if (!isInsideQuotes && line[i] == '"')
                {
                    isInsideQuotes = true;
                    start = i + 1;
                }
                else if (isInsideQuotes && line[i] == '"' && (i == line.Length -1 || i < line.Length -1 && line[i+1] != '"' )){
                    isInsideQuotes = false;
                    
                    chunks.Add( line.Substring(start, i- start ).Replace("\"\"","\""));
                }
            }
            return chunks.ToArray();
        }
        public static TransferGraph loadCommentTransferGraph(string fileName)
        {
            string entitiesFilename, transfersFilename;

            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
            {
                string directory = System.IO.Path.GetDirectoryName(fileName);
                entitiesFilename = directory + "\\" + file.ReadLine();
                transfersFilename = directory + "\\" + file.ReadLine();
            }

            TransferGraph tg = CommentTransferGraph.LoadCommentTransferGraph(
                entitiesFilename,
                transfersFilename,
                new EfficientDepictionIntervalStrategy(new TimeSpan(0, 5, 0)).compute);

            return tg;
        }
        public static TransferGraph LoadCommentTransferGraph(
            string entitiesFilename, 
            string transfersFilename,
            DepictionInterval.DepictionIntervalSelection dis ){
            
            List<Entity> entities = new List<Entity>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(entitiesFilename))
            {
                //ignore the header we assume that it is
                //name, role, expected score
                file.ReadLine();
                int id = 0;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var chunks = splitCSVLine(line);
                    if (chunks.Length == 0)
                    {
                        continue;
                    }
                    Entity e = new Entity( 
                        id: id++,
                        key: chunks[0],
                        attributes: new CommentEntityAttributes(
                            name: chunks[0],
                            role: chunks[1],
                            expectedScore: float.Parse(chunks[2])));
                    
                    entities.Add(e);
                }
            }

            //load the transfer
            List<Transfer> transfers = new List<Transfer>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(transfersFilename))
            {
                //ignore the header we assume that it is
                //"source", "destination", "startingTime", "finishingTime", "score", "size", "message", "thread", "href", "parent",
                file.ReadLine();

                int id = 0;

                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var chunks = splitCSVLine(line);
                    if (chunks.Length == 0)
                    {
                        continue;
                    }

                    CommentTransferAttributes attributes = new CommentTransferAttributes(
                            score: int.Parse(chunks[4]),
                            size: int.Parse(chunks[5]),
                            message: chunks[6],
                            thread: chunks[7],
                            href: chunks[8],
                            parentHref: chunks[9]);

                    Transfer r = new Transfer(
                        id: id++,
                        source: entities.Find(e => e.key == chunks[0]),
                        destination: entities.Find(e => e.key == chunks[1]),
                        ts: DateTime.ParseExact(chunks[2], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                        tf: DateTime.ParseExact(chunks[3], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                        attributes:attributes);

                    attributes.transfer = r;

                    transfers.Add(r);
                }
            }
            
            Transfer[] transfersArr = transfers.ToArray();
            Entity[] entitiesArr = entities.ToArray();


            for (int i = 0; i < entitiesArr.Length; i++)
            {
                Entity e = entitiesArr[i];
                e.di = dis(e, transfersArr);
            }

            TransferGraph g = new TransferGraph(entitiesArr, transfersArr);

            //update the comment attributes
            foreach (var transfer in g.transfers)
            {
                var comment = transfer.attributes as CommentTransferAttributes;
                var parent = getComment(comment.parentHref, g);
                comment.parent = parent;

                if (parent != null && parent != comment)
                {
                    parent.children.Add(comment);
                }
            }


            foreach (var transfer in g.transfers)
            {
                var comment = transfer.attributes as CommentTransferAttributes;
                comment.children.Sort((a, b) => a.transfer.ts.CompareTo(b.transfer.ts));
            }
            return g;
        }

        private static CommentTransferAttributes getComment( string href, TransferGraph g)
        {
            return g.transfers
                .Select(tr => (tr.attributes as CommentTransferAttributes))
                .FirstOrDefault( c => c.href == href);
        }

    }
}
