using CarcassonneServer.Model.Representation.SubAreas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public class RoadArea : BaseArea
    {
        public override AreaType AreaType => AreaType.Road;

        protected RoadArea(int id, ISubArea subArea)
            : base()
        {
            this.id = id;
            AddSubArea(subArea);
        }
        /// <summary>
        /// Mező hozzáadása területhez.
        /// </summary>
        /// <param name="subArea">Mező amit hozzá akarunk adni a területhez.</param>
        public override void AddSubArea(ISubArea subArea) => base.AddSubArea(subArea);

        public override void RemoveSubArea(ISubArea subArea)
        {
            base.RemoveSubArea(subArea);

            if (OpenSubAreas.Contains(subArea))
                OpenSubAreas.Remove(subArea);

            if (SurroundedSubAreas.Contains(subArea))
                SurroundedSubAreas.Remove(subArea);

            subAreas.Remove(subArea);
        }
        protected override bool EvaluateIsFinished() => base.EvaluateIsFinished();

        protected override bool IsNeighbourTo(Position element) => OpenSubAreas.Any(os => os.Parent | element);

        protected override bool IsNeighbourTo(BaseArea area) => OpenSubAreas.Any(os => area.SubAreas.Any(s => os.IsAdjacent(s)));

        override public bool CanAdd(ISubArea subArea) => base.CanAdd(subArea);

        private static int currentId;
        public static new RoadArea Get(ISubArea initialArea) => new RoadArea(++currentId, initialArea);
        public override int Score => Positions.Count;
    }
}
