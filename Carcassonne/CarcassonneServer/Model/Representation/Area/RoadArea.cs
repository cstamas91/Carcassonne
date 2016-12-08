using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public class RoadArea : BaseArea
    {
        public override AreaType AreaType { get { return AreaType.Road; } }

        public override bool IsFinished { get { return EvaluateIsFinished(); } }
        
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

            //szomszédos belső elemek kezelése
            List<SubArea> adjacentSubAreas = OpenSubAreas.Where(item => item | subArea).ToList();

            adjacentSubAreas.ForEach(item =>
            {
                if (IsSurrounded(item))
                {
                    OpenSubAreas.Remove(item);
                    SurroundedSubAreas.Add(item);
                }
            });

            //hozzáadandó elem kezelése
            subAreas.Add(subArea);
            if (IsSurrounded(subArea))
                SurroundedSubAreas.Add(subArea);
            else
                OpenSubAreas.Add(subArea);
        }

        /// <summary>
        /// Kiértékeli, hogy egy mező körbe van-e véve a területhez tartozó többi mezővel, vagy van még szabad oldala.
        /// </summary>
        /// <param name="item">A vizsgált részterület.</param>
        /// <returns>A vizsgált részterület be van-e kerítve vagy nem.</returns>
        private bool IsSurrounded(SubArea item)
        {
            List<SubArea> adjacents = SubAreas.Where(area => area | item).ToList();
            return item.Edges.All(edge => adjacents.Any(adjacent => adjacent.Position.DirectionTo(item.Position) == edge));
        }

        /// <summary>
        /// Beállítja a kapott mező megfelelő oldalak GUIDjainak a konstrukció guidját.
        /// </summary>
        /// <param name="element">A menedzselendő mező.</param>
        private void ManageGuids(Tile element, params Direction[] sideDirections)
        {
            throw new NotImplementedException();
        }

        public override void AddMeeple(Meeple meeple, SubArea subArea)
        {
            if (meeples.Count > 0)
                throw new InvalidOperationException();

            meeples.Add(meeple);
        }

        protected override bool EvaluateIsFinished()
        {
            return OpenSubAreas.Count() == 0;
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
