namespace CarcassonneServer.Model.Representation
{
    public abstract class BaseTileCollection
    {
        virtual public int Count { get; }
        virtual public void Add(Tile tile) { }
    }

    public class LinkedTileCollection : BaseTileCollection
    {
        private class LinkedTileNode
        {
            public Tile Value { get; private set; }
            public LinkedTileNode Next { get; set; }
            public LinkedTileNode Prev { get; set; }

            public LinkedTileNode(Tile value, LinkedTileNode prev, LinkedTileNode next)
            {
                Value = value;
                Prev = prev;
                Next = next;
            }
        }

        private LinkedTileNode head = null;
        private LinkedTileNode current = null;

        public override int Count
        {
            get
            {
                int count = 0;
                LinkedTileNode step = head;
                while (step != current)
                {
                    count++;
                    step = step.Next;
                }

                return count;
            }
        }

        public LinkedTileCollection()
        {

        }

        public LinkedTileCollection(Tile tile)
        {
            Add(tile);
        }

        public override void Add(Tile tile)
        {
            var newNode = new LinkedTileNode(tile, current, null);

            if (head == null)
            {
                head = newNode;
                current = newNode;
            }
            else
            {
                current.Next = newNode;
                current = newNode;
            }
        }
    }

    public class GraphTileCollection : BaseTileCollection
    {
        private class GraphTileNode
        {
            Tile value;
            GraphTileNode Parent { get; set; }
            GraphTileNode Left { get; set; }
            GraphTileNode Right { get; set; }
            GraphTileNode Down { get; set; }
        }

        private int count;
        public override int Count
        {
            get
            {
                return count;
            }
        }
    }
}