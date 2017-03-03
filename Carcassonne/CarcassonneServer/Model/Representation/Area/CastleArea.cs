using CarcassonneServer.Model.Representation.SubAreas;
using System;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public class CastleArea : BaseArea
    {
        public override AreaType AreaType => AreaType.Castle;
        protected CastleArea(int id, ISubArea subArea)
        {
            this.id = id;
            AddSubArea(subArea);
            SortSubAreas();
        }                                                  

        override public void AddSubArea(ISubArea subArea) => base.AddSubArea(subArea);
                                      
        protected override bool EvaluateIsFinished() => base.EvaluateIsFinished();

        override protected bool IsNeighbourTo(BaseArea area)
        {
            throw new NotImplementedException();
        }

        override protected bool IsNeighbourTo(Position element)
        {
            throw new NotImplementedException();
        }

        override public bool CanAdd(ISubArea subArea) => base.CanAdd(subArea);

        private static int currentId;
        public static new CastleArea Get(ISubArea initialArea) => new CastleArea(++currentId, initialArea);

        public override int Score => Positions.Count * 2 + meeples.Count(meeple => meeple.Owner == Owners.FirstOrDefault()) * 2;
    }
}
