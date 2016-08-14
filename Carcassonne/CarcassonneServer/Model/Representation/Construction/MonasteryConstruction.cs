using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public class MonasteryConstruction : BaseConstruction
    {
        private Tile monastery = null;

        public MonasteryConstruction(ref Tile tile)
        {
            this.AddElement(ref tile);
        }                             

        public override void AddElement(ref Tile element)
        {
            throw new NotImplementedException();
        }

        public override void AddMeeple(Meeple meeple)
        {
            if (meeples.Count == 1)
                throw new InvalidOperationException();

            meeples.Add(meeple);
        }

        public override TileSideType AreaType { get { return TileSideType.Field; } }

        private List<Tile> edgeTiles = new List<Tile>();
        public override bool IsFinished { get { return elements.Count == 9; } }

        protected override bool IsNeighbourTo(BaseConstruction construction)
        {
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourTo(Position element)
        {
            return monastery | element;
        }
    }
}
