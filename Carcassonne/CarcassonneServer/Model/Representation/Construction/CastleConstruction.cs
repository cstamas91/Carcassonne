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

            #region 0.
            foreach (var key in elements.Keys)
                if (elements.Values.Count(tileList => tileList.Count(tile => tile | tileToAdd) == 0) == Enum.GetValues(typeof(TileTag)).Length)
                    throw new ArgumentException("A mező nem szomszédos az építménnyel, így nem lehet hozzáadni.");
            #endregion 0.

            #region 1.
            IEnumerable<Tile> neighboringTiles = from ICollection<Tile> list in elements.Values
                                                 from Tile tile in list
                                                 where tile | tileToAdd
                                                 select tile;

            IEnumerable<TileSideDescriptor> connectingSides = from tile in neighboringTiles
                                                              select tileToAdd[tileToAdd.NeighborDirection(tile)];

            if (connectingSides.Any(side => side.Closed))
                elements[TileTag.Border].Add(tileToAdd);

            if (neighboringTiles.Count() < 4)
                elements[TileTag.Edge].Add(tileToAdd);

            if (neighboringTiles.Count() == 4)
                elements[TileTag.Inner].Add(tileToAdd);
            #endregion 1.

            #region 2.
            foreach (TileSideDescriptor side in connectingSides)
                side.ConstructionGuid = this.GUID;
            #endregion 2.

            #region 3.
            IEnumerable<Tile> falseEdgeTags = from tile in elements[TileTag.Inner]
                                               where elements[TileTag.Inner].NumberOfNeighborsTo(tile) == 4
                                               select tile;

            foreach(Tile tile in falseEdgeTags)
            {
                elements[TileTag.Edge].Remove(tile);
                elements[TileTag.Inner].Add(tile);
            }
            #endregion 3.
        }

        /// <summary>
        /// Kezelni kell az élmezőket:
        ///   - edgeTiles-ból lekérni a hozzáadottal szomszédos mezőket
        ///   - mindegyiket kiértékelni, hogy be be vannak-e kerítve
        ///   - amelyik be van kerítve, azt eltávolítani edgeTiles-ból
        /// </summary>
        /// <param name="tileToAdd">Az éppen hozzáadott mező, ennek a pozíciója változtathatja meg az élmezők halmazát.</param>
        private void ManageEdgeTiles(Tile tileToAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Egy adott mezővel szomszédos mezőkből álló felsorolót állít elő. 
        /// </summary>
        /// <param name="tile">A mező, amivel szomszédos mezőket ki akarjuk gyűjteni.</param>
        /// <returns>IEnumerable<Tile> ami a kapott mezővel szomszédos mezőket sorolja fel.</Tile></returns>
        private IEnumerable<Tile> GetNeighboringElementsFor(Tile tile)
        {
            throw new NotImplementedException();
        }
        
        /// <summary> A kapott mezővel szomszédos mezők számát adja meg. </summary>
        /// <param name="tile"> A vizsgált mező. </param>
        /// <returns> A vizsgált mezővel szomszédos mezők száma.</returns>
        private int GetNeighboringElementCount(Tile tile)
        {
            return GetNeighboringElementsFor(tile).Count();
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

        protected override bool EvaluateIsFinished()
        {
            throw new NotImplementedException();
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
