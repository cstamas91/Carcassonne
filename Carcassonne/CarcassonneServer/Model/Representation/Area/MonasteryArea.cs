using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public class MonasteryArea : BaseArea
    {
        private Tile monastery = null;

        public MonasteryArea(Tile tile)
        {
            this.AddSubArea(tile);
        }                             

        public override void AddSubArea(Tile element)
        {
            throw new NotImplementedException();
        }

        public override void AddMeeple(Meeple meeple)
        {
            if (meeples.Count == 1)
                throw new InvalidOperationException();

            meeples.Add(meeple);
        }

        public override AreaType AreaType { get { return AreaType.Field; } }

        private List<Tile> edgeTiles = new List<Tile>();
        public override bool IsFinished { get { return elements.Count == 9; } }

        protected override bool IsNeighbourTo(BaseArea area)
        {
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourTo(Position element)
        {
            return monastery | element;
        }
    }
}
