using System;
using System.Collections.Generic;
using System.Linq;
namespace CarcassonneServer.Model.Representation.Construction
{
    public class CastleConstruction : BaseConstruction
    {
        public override TileSideType AreaType { get { return TileSideType.Castle; } }

        private bool? isFinished;
        public override bool IsFinished
        {
            get
            {
                if (!isFinished.HasValue)
                    isFinished = EvaluateIsFinished();

                return isFinished.Value;
            }
        }

        public CastleConstruction(Tile startTile = null)
        {
        }
        /// <summary>
        /// Hozzáad egy mezőt a konstrukcióhoz.
        /// </summary>
        /// <param name='tileToAdd'>A hozzáadandó mező.</param>
        public override void AddElement(Tile tileToAdd)
        {
            /* 0. Ha a mező nem is szomszédos az építménnyel, kivételt kell dobni, valami gáz van.
             * 1. Meg kell állapítani, hogy a hozzáadandó mező milyen cédulát kapjon.
             * 2. A mező megfelelő oldalának leírójában be kell állítani az építmény GUIDját.
             * 3. Ellenőrizni kell minden más mezőt, hogy címkeváltozás aktuális-e, ha igen, áthelyezni a megfelelő csoportba.
             *      - Élmező bekerítődött => át kell helyezni a belső mezők közé
             *      - Belső mező nem változhat
             *      - Határmező nem változhat */


            foreach (var key in elements.Keys)
                if (elements.Values.Count(tileList => tileList.Count(tile => tile | tileToAdd) == 0) == Enum.GetValues(typeof(TileTag)).Length)
                    throw new ArgumentException("A mező nem szomszédos az építménnyel, így nem lehet hozzáadni.");

            
            IEnumerable<Tile> neighboringTiles = elements.Values.
                                     Select(item => item.GetNeighboringTiles(tileToAdd)).
                                     Aggregate((fst, snd) => fst.Concat(snd));

            IEnumerable<TileSideDescriptor> connectingSides = from tile in neighboringTiles
                                                              select tileToAdd[tileToAdd.NeighborDirection(tile)];

            ManageTagsForTileToAdd(tileToAdd, neighboringTiles, connectingSides);

            
            foreach (TileSideDescriptor side in connectingSides)
                side.ConstructionGuid = this.GUID;

            
            IEnumerable<Tile> falseEdgeTags = from tile in elements[TileTag.Inner]
                                              where elements[TileTag.Inner].NumberOfNeighborsTo(tile) == 4
                                              select tile;

            foreach (Tile tile in falseEdgeTags)
            {
                elements[TileTag.Edge].Remove(tile);
                elements[TileTag.Inner].Add(tile);
            }

        }
        /// <summary>
        /// Elhelyezi a hozzáadandó mezőt a szótár megfelelő kollekcióiban.
        /// </summary>
        /// <param name="tileToAdd">A hozzáadandó mező.</param>
        private void ManageTagsForTileToAdd(Tile tileToAdd, IEnumerable<Tile> neighboringTiles, IEnumerable<TileSideDescriptor> connectingSides)
        {
            if (neighboringTiles.Count() > 4)
                throw new UnrealisticResultException("Négynél több oldala nem lehet szomszédos egy mezőnek.");

            if (connectingSides.Any(side => side.Closed))
                elements[TileTag.Border].Add(tileToAdd);

            if (neighboringTiles.Count() < 4)
                elements[TileTag.Edge].Add(tileToAdd);

            if (neighboringTiles.Count() == 4)
                elements[TileTag.Inner].Add(tileToAdd);
        }

        /// <summary>Elhelyezi a kapott figurát a konstrukción.</summary>
        /// <param name="meeple">Elhelyezendő figura.</param>
        public override void AddMeeple(Meeple meeple)
        {
            if (!IsFinished && meeples.Count == 0)
                meeples.Add(meeple);
            else
                throw new InvalidOperationException();
        }

        ///<summary>Összeolvasztja a példányt a kapott konstrukcióval.</summary>
        ///<param name="other">A másik konstrukció</param>
        ///<returns>Az összeolvasztás után kapott konstrukció</returns>
        public override BaseConstruction Merge(BaseConstruction other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kiértékeli, hogy az építmény befejeződött-e.
        /// </summary>
        /// <returns>Az építmény befejeződött-e.</returns>
        protected override bool EvaluateIsFinished()
        {
            return !EdgeTilesExist() && AreBorderTilesClosed();
        }

        /// <summary>
        /// Kiértékeli, hogy létezik-e olyan belső mező, ami nincs teljesen körbevéve.
        /// </summary>
        /// <returns>Van-e olyan mező, ami nincs teljesen körbevéve.</returns>
        private bool EdgeTilesExist()
        {
            return elements[TileTag.Edge].Any();
        }

        /// <summary>
        /// Kiértékeli, hogy az építményt szegélyező mezők zárt kört alkotnak-e.
        /// </summary>
        /// <returns>A szegély mezők zárt kört alkotnak-e.</returns>
        private bool AreBorderTilesClosed()
        {
            /* Bejárjuk a szegélyező mezőket. Ha sikerül nemtriviálisan körbeérnünk, akkor a mezők zárt láncot alkotnak.*/
            IEnumerable<Tile> borders = elements[TileTag.Border];
            Tile first = borders.First();
            Tile prev = first;
            Tile current = borders.First(item => item | first);

            while (true)
            {
                Tile temp = current;
                current = GetNextBorderTile(current, prev);
                prev = temp;

                if (current == null)
                    return false;

                if (current == first)
                    return true;
            }
        }
        /// <summary>
        /// Visszaadja a bejárás soron következő elemét, odafigyelve arra a speciális esetre, ha a láncban több lehetséges következő lépés is létezik.
        /// Ekkor a döntéshez fel kell használni a szomszédság irányát.
        /// </summary>
        /// <param name="current">A mező amin vagyunk.</param>
        /// <param name="prev">Az előzőleg bejárt mező.</param>
        /// <returns></returns>
        private Tile GetNextBorderTile(Tile current, Tile prev)
        {
            IEnumerable<Tile> borders = elements[TileTag.Border];

            if (borders.Count(item => item | current && item != prev) > 1)
            {
                Direction nextDirection = current.NeighborDirection(prev).Opposite();
                IEnumerable<Tile> candidates = borders.Where(item => item | current && item != prev);
                return candidates.First(item => current.NeighborDirection(item) == nextDirection);
            }

            return borders.First(item => item | current && item != prev);
        }

        protected override bool IsNeighbourTo(BaseConstruction construction)
        {
            return (from tile in EdgeTiles
                    where tile | construction
                    select tile).Count() > 0;
        }

        protected override bool IsNeighbourTo(Position element)
        {
            return (from tile in EdgeTiles
                    where tile | element
                    select tile).Count() > 0;
        }
    }
}
