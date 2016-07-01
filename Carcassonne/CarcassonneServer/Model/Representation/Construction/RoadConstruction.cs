using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public class RoadConstruction : BaseConstruction
    {
        public override TileSideType AreaType { get { return TileSideType.Road; } }

        public override bool IsFinished { get { throw new NotImplementedException(); } }

        private Tile start = null;
        private Tile end = null;

        public override IEnumerable<Tile> EdgeTiles { get { return new List<Tile>() { start, end }.AsEnumerable(); } }

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
            elements.Add(element);

            if (elements.Count == 1)
            {
                // Ez akkor történik, ha éppen egy újonnan létrehozott úthoz adjuk hozzá az első elemet.
                start = element;
                end = element;
            }
            else if (elements.Count == 2)
            {
                // Ez a második elem hozzáadásakor történik. Ekkor az út eleje és vége egyezik, ezért az újonnan hozzáadottat vesszük a végének. (az út iránya irreleváns)
                end = element;
                ManageGuids(element);
            }
            else
            {
                if (element | start)
                    start = element;
                else if (element | end)
                    end = element;

                //Ha egyik végével sem szomszédos, akkor hiba történt.
                elements.Remove(element);
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Beállítja a kapott mező megfelelő oldalak GUIDjainak a konstrukció guidját.
        /// </summary>
        /// <param name="element">A menedzselendő mező.</param>
        private void ManageGuids(Tile element, params Direction[] sideDirections)
        {

            //kiválasztjuk a konstrukcióban lévő szomszédos mezőket, melyek meg tudják mondani, hogy tőlük melyik irányban van a kérdéses mező
            //a kapott irányokkal ellenkező oldalakat kell beállítani a mezőben.
            var neighboringSides = from tile in elements
                                   where tile | element
                                   select tile.NeighborDirection(element).Opposite();

            foreach (var direction in neighboringSides)
            {
                if (element[direction].Type != AreaType)
                    throw new InvalidOperationException();

                element[direction].ConstructionGuid = GUID;
            }
        }

        public override void AddMeeple(Meeple meeple)
        {
            if (meeples.Count > 0)
                throw new InvalidOperationException();

            meeples.Add(meeple);
        }

        protected override bool NeighbourTo(Position element)
        {
            return start | element || end | element;
        }

        protected override bool NeighbourTo(BaseConstruction construction)
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
            RoadConstruction otherRoad = other as RoadConstruction;

            if (otherRoad == null || !(this | otherRoad))
                throw new InvalidOperationException();

            foreach (var item in otherRoad.elements)
                AddElement(item);

            return this;
        }
    }
}
