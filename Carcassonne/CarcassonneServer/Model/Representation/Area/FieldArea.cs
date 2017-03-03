using CarcassonneServer.Model.Representation.SubAreas;
using System;

namespace CarcassonneServer.Model.Representation.Area
{
    public class FieldArea : BaseArea
    {
        public override AreaType AreaType => AreaType.Field;

        protected FieldArea(int id, ISubArea initialArea)
        {
            this.id = id;
            AddSubArea(initialArea);
        }

        public override void AddSubArea(ISubArea subArea)
        {
            base.AddSubArea(subArea);
        }

        protected override bool EvaluateIsFinished() => base.EvaluateIsFinished();

        override public bool CanAdd(ISubArea subArea) => base.CanAdd(subArea);

        private static int currentId;
        public static new FieldArea Get(ISubArea initialArea) => new FieldArea(++currentId, initialArea);
    }
}
