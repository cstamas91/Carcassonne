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

        /// <summary>
        /// Alapértelmezett konstruktor.
        /// </summary>
        public RoadArea() : base()
        {
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

            //hozzáadandó elem kezelése
            subAreas.Add(subArea);
            SurroundedSubAreas = new List<SubArea>();
            OpenSubAreas = new List<Representation.SubArea>();
            subAreas.ForEach(SortArea);
        }

        private void SortArea(SubArea subArea)
        {
            if (IsSurrounded(subArea))
                SurroundedSubAreas.Add(subArea);
            else
                OpenSubAreas.Add(subArea);
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

        /// <summary>
        /// Kiértékeli, hogy egy mező körbe van-e véve a területhez tartozó többi mezővel, vagy van még szabad oldala.
        /// </summary>
        /// <param name="item">A vizsgált részterület.</param>
        /// <returns>A vizsgált részterület be van-e kerítve vagy nem.</returns>
        private bool IsSurrounded(SubArea item)
        {
            bool isSurrounded = true;
            foreach (Direction d in item.Edges)
            { 
                try
                {
                    isSurrounded &= Positions.Contains(item.Parent.GetPosition(d));
                }
                catch (OutOfBoundsException oobEx)
                {
                    if (!oobEx.Position.IsBounded)
                        throw;
                    isSurrounded &= false;
                }
            }

            return isSurrounded;
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
            throw new NotImplementedException();
        }

        protected override bool IsNeighbourTo(BaseArea area)
        {
            throw new NotImplementedException();
        }

        public override Direction NeighborDirection(Position other)
        {
            throw new NotImplementedException();
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
