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
            subAreas.Add(subArea);
            SortSubAreas();
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
