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

        public CastleArea(SubArea subArea)
        {
            AddSubArea(subArea);
            SortSubAreas();
        }
        /// <summary>
        /// Hozzáad egy mezőt a konstrukcióhoz.
        /// </summary>
        /// <param name='tileToAdd'>A hozzáadandó mező.</param>
        override public void AddSubArea(SubArea subArea)
        {
            base.AddSubArea(subArea);
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
        /// <summary>
        /// Kiértékeli, hogy az építmény befejeződött-e.
        /// </summary>
        /// <returns>Az építmény befejeződött-e.</returns>
        protected override bool EvaluateIsFinished()
        {
            return OpenSubAreas.Count == 0;
        }

        override protected bool IsNeighbourTo(BaseArea area)
        {
            throw new NotImplementedException();
        }

        override protected bool IsNeighbourTo(Position element)
        {
            throw new NotImplementedException();
        }

        protected override bool CanAdd(SubArea subArea)
        {
            if (OpenSubAreas.Count > 0 && !OpenSubAreas.Any(osa => osa | subArea))
                throw new TileAddException("Failed CANADD");

            IEnumerable<SubArea> borders = OpenSubAreas.Where(osa => osa | subArea);
            return borders.All(border => border.CanBeAdjacent(subArea));
        }
    }
}
