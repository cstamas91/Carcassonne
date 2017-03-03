using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation.SubAreas
{
    public class CastleSubArea : BaseSubArea
    {
        public override AreaType AreaType => AreaType.Castle;
        public override int Score => 0;

        protected CastleSubArea(int id, IList<Direction> directions)
            : base(id, directions) { }

        private static int currentIndex = 0;
        public static CastleSubArea Get(IList<Direction> directions)
        {
            return new CastleSubArea(++currentIndex, directions);
        }
    }
}
