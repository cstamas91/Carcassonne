using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public class RoadConstruction : BaseConstruction
    {
        public override TileSideType AreaType { get { return TileSideType.Road; } }

        public override bool IsFinished { get { return EvaluateIsFinished(); } }

        private Tile start = null;
        private Tile end = null;

        public RoadConstruction()
        {
        }

        public RoadConstruction(Tile tile, params Direction[] sideDirections)
        {
            foreach (var param in sideDirections)
                tile[param].ConstructionGuid = GUID;

            AddElement(tile);

            start = tile;
            end = tile;
        }

        public override void AddElement(Tile element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Beállítja a kapott mező megfelelő oldalak GUIDjainak a konstrukció guidját.
        /// </summary>
        /// <param name="element">A menedzselendő mező.</param>
        private void ManageGuids(Tile element, params Direction[] sideDirections)
        {
        }

        public override void AddMeeple(Meeple meeple)
        {
            if (meeples.Count > 0)
                throw new InvalidOperationException();

            meeples.Add(meeple);
        }

        protected override bool EvaluateIsFinished()
        {
            var connectedSides = from edgeTile in EdgeTiles
                                 from direction in Enum.GetValues(typeof(Direction)) as IEnumerable<Direction>
                                 where edgeTile[direction].ConstructionGuid == this.GUID
                                 select edgeTile[direction];

            return connectedSides.All(sideDescriptor => sideDescriptor.Closed);
        }

        protected override bool IsNeighbourTo(Position element)
        {
            return start | element || end | element;
        }

        protected override bool IsNeighbourTo(BaseConstruction construction)
        {
            if (construction.AreaType != this.AreaType)
                throw new InvalidOperationException();

            return (from thisTile in EdgeTiles
                    from otherTile in construction.EdgeTiles
                    where thisTile | otherTile
                    select true).Count() > 0;
        }

        public override Direction NeighborDirection(Position other)
        {
            return start | other ? start.NeighborDirection(other) : end.NeighborDirection(other);
        }
        /// <summary>
        /// Két út összeépítésére szolgáló eljárás.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Hamissal, ha a két út nem szomszédos, egyébként igazzal, ha az összeolvasztás sikeres.</returns>
        public override BaseConstruction Merge(BaseConstruction other)
        {
            throw new NotImplementedException();
        }
    }
}
