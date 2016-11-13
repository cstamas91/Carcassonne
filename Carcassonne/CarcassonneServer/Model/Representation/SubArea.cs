using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation
{
    public abstract class SubArea
    {
        private Tile parent;
        private List<Direction> edges = new List<Direction>();
        public List<Direction> Edges { get; set; }

        public virtual int Points { get; set; }

        public bool this[Direction direction]
        {
            get
            {
                return Edges.Contains(direction);
            }
        }

        private AreaType areaType;
        public AreaType AreaType
        {
            get
            {
                return areaType;
            }
        }
        private Meeple meeple { get; set; }
        public Meeple Meeple
        {
            get
            {
                return meeple;
            }
        }

        public Position Position
        {
            get
            {
                return parent;
            }
        }

        public SubArea(IList<Direction> governedEdges, AreaType areaType, Tile parent)
        {
            this.parent = parent;
            this.edges = governedEdges.ToList();
            this.areaType = areaType;
        }
    }
}