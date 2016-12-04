using System;
using System.Collections.Generic;
using System.Linq;
namespace CarcassonneServer.Model.Representation.Area
{
    public class CastleArea : BaseArea
    {
        public override AreaType AreaType { get { return AreaType.Castle; } }

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

        public CastleArea(Tile startTile = null)
        {
        }
        /// <summary>
        /// Hozzáad egy mezőt a konstrukcióhoz.
        /// </summary>
        /// <param name='tileToAdd'>A hozzáadandó mező.</param>
        public void AddSubArea(Tile tileToAdd)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Elhelyezi a hozzáadandó mezőt a szótár megfelelő kollekcióiban.
        /// </summary>
        /// <param name="tileToAdd">A hozzáadandó mező.</param>
        private void ManageTagsForTileToAdd(Tile tileToAdd, IEnumerable<Tile> neighboringTiles, IEnumerable<TileSideDescriptor> connectingSides)
        {
            throw new NotImplementedException();
        }

        /// <summary>Elhelyezi a kapott figurát a konstrukción.</summary>
        /// <param name="meeple">Elhelyezendő figura.</param>
        public void AddMeeple(Meeple meeple)
        {
            if (!IsFinished && meeples.Count == 0)
                meeples.Add(meeple);
            else
                throw new InvalidOperationException();
        }

        ///<summary>Összeolvasztja a példányt a kapott konstrukcióval.</summary>
        ///<param name="other">A másik konstrukció</param>
        ///<returns>Az összeolvasztás után kapott konstrukció</returns>
        public override BaseArea Merge(BaseArea other)
        {
            throw new NotImplementedException();
        }

        private void ManageFalseInnerTiles()
        {
            throw new NotImplementedException();
        }

        private void MoveFalseInnerTile(Tile tile)
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kiértékeli, hogy az építményt szegélyező mezők zárt kört alkotnak-e.
        /// </summary>
        /// <returns>A szegély mezők zárt kört alkotnak-e.</returns>
        private bool AreBorderTilesClosed()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourTo(BaseArea area)
        {
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourTo(Position element)
        {
            throw new NotImplementedException();
        }
    }
}
