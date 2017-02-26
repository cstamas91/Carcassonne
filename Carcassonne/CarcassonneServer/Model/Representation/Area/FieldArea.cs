namespace CarcassonneServer.Model.Representation.Area
{
    /* Elkészültség feltétele: minden benne lévő elem szomszédos egy másik benne lévő elemmel
     */
    public class FieldArea : BaseArea
    {
        public FieldArea(SubArea initialArea)
        {
            AddSubArea(initialArea);
        }

        public override void AddSubArea(SubArea subArea)
        {
            base.AddSubArea(subArea);

            subAreas.Add(subArea);
            SortSubAreas();
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
    }
}
