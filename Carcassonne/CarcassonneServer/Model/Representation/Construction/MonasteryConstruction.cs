using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public class MonasteryConstruction : BaseConstruction
    {
        private Tile monastery = null;

        public MonasteryConstruction(Tile tile)
        {
            AddElement(tile);
        }                             

        public override void AddElement(Tile element)
        {
            if (elements.Count == 0)
                monastery = element;
            else if (!(this | element)) //így nem tudunk a monostorral nem szomszédos elemet hozzáadni.
                throw new InvalidOperationException();

            elements.Add(element);
        }

        public override void AddMeeple(Meeple meeple)
        {
            if (meeples.Count == 1)
                throw new InvalidOperationException();

            meeples.Add(meeple);
        }

        public override TileSideType AreaType { get { return TileSideType.Field; } }

        private List<Tile> edgeTiles = new List<Tile>();
        public override IEnumerable<Tile> EdgeTiles { get { return edgeTiles.AsEnumerable(); } }
        public override bool IsFinished { get { return elements.Count == 9; } }

        protected override bool NeighbourTo(BaseConstruction construction)
        {
            return false;
        }

        protected override bool NeighbourTo(Position element)
        {
            return monastery | element;
        }
    }
}
