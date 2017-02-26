using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public class RoadArea : BaseArea
    {
        public override AreaType AreaType
        {
            get
            {
                return AreaType.Road;
            }
        }

        public override bool IsFinished
        {
            get
            {
                return EvaluateIsFinished();
            }
        }

        public override int Score
        {
            get
            {
                return SubAreas.Sum(a => a.Points);
            }
        }

        public RoadArea(SubArea subArea)
            : base()
        {
            AddSubArea(subArea);
        }
        /// <summary>
        /// Mező hozzáadása területhez.
        /// </summary>
        /// <param name="subArea">Mező amit hozzá akarunk adni a területhez.</param>
        public override void AddSubArea(SubArea subArea)
        {
            base.AddSubArea(subArea);
            subAreas.Add(subArea);
            SortSubAreas();
        }

        public override void RemoveSubArea(SubArea subArea)
        {
            base.RemoveSubArea(subArea);

            if (OpenSubAreas.Contains(subArea))
                OpenSubAreas.Remove(subArea);

            if (SurroundedSubAreas.Contains(subArea))
                SurroundedSubAreas.Remove(subArea);

            subAreas.Remove(subArea);
        }

        public override void AddMeeple(Meeple meeple, SubArea subArea)
        {
            if (meeples.Count == 0)
                meeples.Add(meeple);
        }

        protected override bool EvaluateIsFinished()
        {
            return OpenSubAreas.Count == 0;
        }

        protected override bool IsNeighbourTo(Position element)
        {
            return OpenSubAreas.Any(os => os.Parent | element);
        }

        protected override bool IsNeighbourTo(BaseArea area)
        {
            return OpenSubAreas.Any(os => area.SubAreas.Any(s => os | s));
        }
        
        override protected void SortSubArea(SubArea area)
        {
            if (IsSurrounded(area))
                SurroundedSubAreas.Add(area);
            else
                OpenSubAreas.Add(area);
        }
    }
}
