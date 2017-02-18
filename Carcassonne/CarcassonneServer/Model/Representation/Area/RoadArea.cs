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
            if (IsSurrounded(subArea))
                SurroundedSubAreas.Add(subArea);
            else
                OpenSubAreas.Add(subArea);

            //szomszédos belső elemek kezelése
            List<SubArea> adjacentSubAreas = OpenSubAreas.Where(item => item | subArea).ToList();

            adjacentSubAreas.ForEach(adjacentSubArea =>
            {
                if (IsSurrounded(adjacentSubArea))
                {
                    OpenSubAreas.Remove(adjacentSubArea);
                    SurroundedSubAreas.Add(adjacentSubArea);
                }
            });
        }

        public override void RemoveSubArea(SubArea subArea)
        {
            base.RemoveSubArea(subArea);

            if (OpenSubAreas.Contains(subArea))
            {
                OpenSubAreas.Remove(subArea);
            }
            if (SurroundedSubAreas.Contains(subArea))
            {
                SurroundedSubAreas.Remove(subArea);
            }

            subAreas.Remove(subArea);

        }

        /// <summary>
        /// Kiértékeli, hogy egy mező körbe van-e véve a területhez tartozó többi mezővel, vagy van még szabad oldala.
        /// </summary>
        /// <param name="item">A vizsgált részterület.</param>
        /// <returns>A vizsgált részterület be van-e kerítve vagy nem.</returns>
        private bool IsSurrounded(SubArea item)
        {
            List<SubArea> adjacents = SubAreas.Where(area => area | item).ToList();
            return item.Edges.All(edge => adjacents.Any(adjacent => item.Position.DirectionTo(adjacent.Position) == edge));
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
        /// <summary>
        /// Két út összeépítésére szolgáló eljárás.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Hamissal, ha a két út nem szomszédos, egyébként igazzal, ha az összeolvasztás sikeres.</returns>
        public override BaseArea Merge(BaseArea other)
        {
            throw new NotImplementedException();
        }
    }
}
