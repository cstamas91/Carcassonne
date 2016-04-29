using System;
using System.Collections.Generic;
using System.Linq;
namespace CarcassonneServer.Model.Representation.Construction
{
    public class CastleConstruction : BaseConstruction
    {
        public override TileSideType AreaType { get { return TileSideType.Castle; } }

        private List<Tile> edgeTiles;
        private IEnumerable<Tile> InnerTiles { get { return elements.Except(edgeTiles); } }

        public override IEnumerable<Tile> EdgeTiles { get { return edgeTiles; } }

        public override bool IsFinished { get { return base.IsFinished; } }

        public CastleConstruction()
        {
            edgeTiles = new List<Tile>();
        }

        public override void AddElement(Tile element)
        {
            var neighboringTiles = from tile in elements
                                   where tile | element
                                   select tile;

            if (neighboringTiles.Count() == 0)
                throw new InvalidOperationException();

            ManageEdgeTiles(element);
            elements.Add(element);

            if (neighboringTiles.Count() == 4)
                edgeTiles.Add(element);
        }

        /// <summary>
        /// Kezelni kell az élmezőket:
        ///   - edgeTiles-ból lekérni a hozzáadottal szomszédos mezőket
        ///   - mindegyiket kiértékelni, hogy be be vannak-e kerítve
        ///   - amelyik be van kerítve, azt eltávolítani edgeTiles-ból
        /// </summary>
        /// <param name="tileToAdd"></param>
        private void ManageEdgeTiles(Tile tileToAdd)
        {
            //kigyűjtjük a hozzáadandó mezővel határos mezőket
            var neighboringElements = from tile in elements
                                      where tile | tileToAdd
                                      select tile;

            //kigyűjtjük a hozzáadottal határos mezők szomszédait, melyek teljesen körbe vannak véve
            var innerElements = from tile in neighboringElements
                                where this.GetNeighboringElementsFor(tile).Count() == 4
                                select tile;

            //a teljesen körbevett mezőket eltávolítjuk az élmezők listájából
            foreach (var item in innerElements)
                edgeTiles.Remove(item);
        }

        /// <summary>
        /// Egy adott mezővel szomszédos mezőkből álló felsorolót állít elő. 
        /// </summary>
        /// <param name="tile">A mező, amivel szomszédos mezőket ki akarjuk gyűjteni.</param>
        /// <returns>IEnumerable<Tile> ami a kapott mezővel szomszédos mezőket sorolja fel.</Tile></returns>
        private IEnumerable<Tile> GetNeighboringElementsFor(Tile tile)
        {
            return from element in elements
                   where element | tile
                   select element;
        }

        public override void AddMeeple(Meeple meeple)
        {
            if (meeples.Count == 0)
                meeples.Add(meeple);

            throw new InvalidOperationException();
        }

        public override BaseConstruction Merge(BaseConstruction other)
        {
            throw new NotImplementedException();
        }

        protected override bool EvaluateIsFinished()
        {
            return base.EvaluateIsFinished();
        }

        protected override bool NeighbourTo(BaseConstruction construction)
        {
            return (from tile in EdgeTiles
                    where tile | construction
                    select tile).Count() > 0;
        }

        protected override bool NeighbourTo(Position element)
        {
            return (from tile in EdgeTiles
                    where tile | element
                    select tile).Count() > 0;
        }
    }
}
