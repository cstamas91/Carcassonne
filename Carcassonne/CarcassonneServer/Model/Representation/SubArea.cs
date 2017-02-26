using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation
{
    public class SubArea
    {
        private Tile parent;
        public Tile Parent { get { return parent; } set { parent = value; } }
        private List<Direction> edges = new List<Direction>();
        public List<Direction> Edges
        {
            get
            {
                return edges;
            }
        }

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

        public SubArea(IList<Direction> governedEdges, AreaType areaType)
        {
            edges = governedEdges.ToList();
            this.areaType = areaType;
        }

        public static bool operator |(SubArea lhs, SubArea rhs) => lhs.parent.IsValidAdjacent(rhs.parent);
        public static bool operator |(SubArea lhs, Tile rhs) => lhs.parent.IsValidAdjacent(rhs);
        public static bool operator |(Tile lhs, SubArea rhs) => rhs | lhs;
    }
}