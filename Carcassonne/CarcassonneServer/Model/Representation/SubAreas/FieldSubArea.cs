using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation.SubAreas
{
    public class FieldSubArea : BaseSubArea
    {
        public override AreaType AreaType => AreaType.Field;
        public override int Score => 0;

        protected FieldSubArea(int id, IList<Direction> directions)
            : base(id, directions) { }

        private static int currentIndex = 0;
        public static FieldSubArea Get(IList<Direction> directions)
        {
            return new FieldSubArea(++currentIndex, directions);
        }
    }
}
