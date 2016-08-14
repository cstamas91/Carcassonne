using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public class RoadConstruction : BaseConstruction
    {
        public override TileSideType AreaType { get { return TileSideType.Road; } }

        public override bool IsFinished { get { return EvaluateIsFinished(); } }

        public RoadConstruction()
        {
        }

        public RoadConstruction(ref Tile tile)
        {
            foreach (TileSideDescriptor side in tile.Sides.Where(item => item.Type == TileSideType.Road))
                side.ConstructionGuid = GUID;

            this.AddElement(ref tile);
        }

        public override void AddElement(ref Tile tileToAdd)
        {
            /* Ha a mező nem szomszédos az úttal, kivételt dobunk.
             * Ellenőrizzük, hogy a mezők valóban egymás mellé helyezhetők-e.
             * Megállapítjuk, hogy milyen cédulát kap az út.
             * Ellenőrizzük, hogy a céldulákat kezelni kell-e. */

            Tile tileToAddNonref = tileToAdd;

            if (elements.Values.Sum(item => item.Count) != 0)
            {

                if (!elements.Values.Aggregate((IEnumerable<Tile> first, IEnumerable<Tile> second) => first.Concat(second)).Any(item => item | tileToAddNonref))
                    throw new ArgumentException("A mező nem szomszédos az úttal, így nem lehet hozzáadni.");

                IEnumerable<Tile> neighboringTiles = elements.Values.Select(item => item.GetNeighboringTiles(tileToAddNonref)).Aggregate((first, second) => first.Concat(second));

                foreach (Tile neighborTile in neighboringTiles)
                {
                    TileSideDescriptor tileToAddConnectingSide = tileToAdd[tileToAdd.NeighborDirection(neighborTile)];
                    TileSideDescriptor neighborTileConnectingSide = neighborTile[neighborTile.NeighborDirection(tileToAdd)];

                    if (tileToAddConnectingSide.Type != neighborTileConnectingSide.Type)
                        throw new TileMatchException("A két mező egymással szembenéző oldalának típusa nem egyezik meg.", tileToAdd, neighborTile);
                }

                IEnumerable<Tile> connectingTileCollection = neighboringTiles.Where(tile => tile[tile.NeighborDirection(tileToAddNonref)].Type == TileSideType.Road);

                if (connectingTileCollection.Count() > 1)
                    throw new ArgumentException();

                Tile connectingTile = connectingTileCollection.First();
                TileSideDescriptor connectingSide = tileToAdd[tileToAdd.NeighborDirection(connectingTile)];

                connectingSide.ConstructionGuid = this.GUID;


                if (connectingSide.Closed)
                {
                    if (elements[TileTag.Border].Contains(connectingTile))
                    {
                        elements[TileTag.Border].Add(tileToAdd);
                    }
                    else
                    {
                        elements[TileTag.Edge].Remove(connectingTile);
                        elements[TileTag.Inner].Add(connectingTile);
                        elements[TileTag.Border].Add(tileToAdd);
                    }
                }
                else
                {
                    if (elements[TileTag.Border].Contains(connectingTile))
                    {
                        elements[TileTag.Edge].Add(tileToAdd);
                    }
                    else
                    {
                        elements[TileTag.Edge].Remove(connectingTile);
                        elements[TileTag.Inner].Add(connectingTile);
                        elements[TileTag.Border].Add(tileToAdd);
                    }
                }
            } 
            else
            {
                if (tileToAdd.Sides.Where(side => side.Type == TileSideType.Road && side.Closed).Any())
                    elements[TileTag.Border].Add(tileToAdd);
                else
                    elements[TileTag.Edge].Add(tileToAdd);
            }
        }

        /// <summary>
        /// Beállítja a kapott mező megfelelő oldalak GUIDjainak a konstrukció guidját.
        /// </summary>
        /// <param name="element">A menedzselendő mező.</param>
        private void ManageGuids(Tile element, params Direction[] sideDirections)
        {
            throw new NotImplementedException();
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
            return elements[TileTag.Edge].Any(item => item | element);
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
            Tile neighbor = elements[TileTag.Edge].First(item => item | other);
            if (neighbor != null)
                return neighbor.NeighborDirection(other);
            else
                throw new ArgumentException(@"Az 'other' elem nem szomszédos az úttal, így nem lehet irányt megállapítani.");
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
