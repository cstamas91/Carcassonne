using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation.SubAreas
{
    public class RoadSubArea : BaseSubArea
    {
        public override AreaType AreaType => AreaType.Road;
        public override int Score => 0;

        protected RoadSubArea(int id, IList<Direction> directions)
            : base(id, directions) { }

        private static int currentIndex = 0;
        public static RoadSubArea Get(IList<Direction> directions)
        {
            return new RoadSubArea(++currentIndex, directions);
        }
    }
}
