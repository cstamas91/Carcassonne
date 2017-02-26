using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public class FieldArea : BaseArea
    {
        public FieldArea(SubArea initialArea)
        {
            AddSubArea(initialArea);
        }

        public override void AddSubArea(SubArea subArea)
        {
            base.AddSubArea(subArea);
        }

        public override bool IsFinished
        {
            get
            {
                return EvaluateIsFinished();
            }
        }

        protected override bool EvaluateIsFinished()
        {
            return OpenSubAreas.Count == 0;
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
