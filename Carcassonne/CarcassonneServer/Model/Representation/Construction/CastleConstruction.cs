using System;
using System.Collections.Generic;
using System.Linq;
namespace CarcassonneServer.Model.Representation.Construction
{
    public class CastleConstruction : BaseConstruction
    {
        public override TileSideType AreaType { get { return TileSideType.Castle; } }

        private TileLinkedList borderTiles;
        private List<Tile> edgeTiles;
        private IEnumerable<Tile> InnerTiles { get { return elements.Except(edgeTiles); } }

        public override IEnumerable<Tile> EdgeTiles { get { return edgeTiles; } }

        public override bool IsFinished { get { return EvaluateIsFinished(); } }

        public CastleConstruction(Tile startTile = null)
        {
            borderTiles = new TileLinkedList();
            edgeTiles = new List<Tile>();
        }
        /// <summary>
        /// Hozzáad egy mezőt a konstrukcióhoz.
        /// </summary>
        /// <param name='element'>A hozzáadandó mező.</param>
        public override void AddElement(Tile element)
        {
            var neighboringTiles = elements.Where(e => e | element);

            if (neighboringTiles.Count() == 0)
                throw new InvalidOperationException();

            ManageEdgeTiles(element);
            elements.Add(element);

            var connectedSide = (Enum.GetValues(typeof(Direction)) as IEnumerable<Direction>)
                                    .Where(d => element[d].ConstructionGuid == GUID)
                                    .Select(d => element[d])
                                    .First();
                
            if (connectedSide != null && connectedSide.Closed)
                borderTiles.Add(element);

            if (neighboringTiles.Count() != 4)
                edgeTiles.Add(element);
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
            //kigyűjtjük a hozzáadandó mezővel határos mezőket
            var tilesNeighboringToTileToAdd = from tile in elements
                                              where tile | tileToAdd
                                              select tile;

            //kigyűjtjük a hozzáadottal határos mezők szomszédait, melyek teljesen körbe vannak véve
            var surroundedEdgeTiles = from tile in tilesNeighboringToTileToAdd
                                      where this.GetNeighboringElementCount(tile) == 4
                                      select tile;

            //a teljesen körbevett mezőket eltávolítjuk az élmezők listájából
            foreach (var item in surroundedEdgeTiles)
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
            return (borderTiles.Head | borderTiles.Current) &&  //elsődleges feltétel
                    CheckMissingInnerField();
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

        private bool CheckMissingInnerField()
        {

            return false;
        }

        private class TileLinkedList
        {
            private class Node
            {
                public Tile value;
                public Node next;

                public Node(Tile value)
                {
                    this.value = value;
                }
            }

            public Tile Head { get { return headNode.value; } }
            public Tile Current { get { return currentNode.value; } }
            Node headNode;
            Node currentNode;

            public void Add(Tile tile)
            {
                if (headNode == null)
                {
                    headNode = new Node(tile);
                    currentNode = headNode;
                }
                else
                {
                    currentNode.next = new Node(tile);
                    currentNode = currentNode.next;
                }
            }
        }
    }
}
