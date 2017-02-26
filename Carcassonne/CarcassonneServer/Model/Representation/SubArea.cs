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
        public IEnumerable<Direction> ActualEdges
        {
            get
            {
                return Edges.Select(direction => direction.GetTileDirectionFromAreaDirection(Parent.Rotation));
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

        public static bool operator |(SubArea lhs, SubArea rhs) => lhs.Parent | rhs.Parent;
        public static bool operator |(SubArea lhs, Tile rhs) => lhs.Parent | rhs;
        public static bool operator |(Tile lhs, SubArea rhs) => rhs | lhs;

        public bool CanBeAdjacent(SubArea other)
        {
            Direction facingMajorDirection = GetDirection(other);
            IEnumerable<Direction> minorDirections = facingMajorDirection.MinorDirections().Where(minorDirection => ActualEdges.Contains(minorDirection));

            return minorDirections.All(minorDirection => other.ActualEdges.Contains(minorDirection.Opposite())) &&
                ActualEdges.Contains(facingMajorDirection) ? other.ActualEdges.Contains(facingMajorDirection.Opposite()) : true;           
        }

        private Direction GetDirection(SubArea other) => Parent.GetDirection(other.Parent);
    }
}