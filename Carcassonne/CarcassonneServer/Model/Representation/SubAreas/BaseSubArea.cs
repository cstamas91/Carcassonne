using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.SubAreas
{
    public class BaseSubArea : ISubArea
    {
        #region properties
        private int id;
        public int Id => id;
        private Tile parent;
        public Tile Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public Position Position => parent;
        private Meeple meeple;
        public Meeple Meeple => meeple;
        virtual public AreaType AreaType { get; }
        private List<Direction> edges;
        public List<Direction> Edges => edges;
        public IEnumerable<Direction> ActualEdges =>
            Edges.Select(
                direction => direction
                    .GetTileDirectionFromAreaDirection(Parent.Rotation));
        public virtual int Score => GetPoints();
        #endregion properties

        #region constructors
        protected BaseSubArea()
        {
            edges = new List<Direction>();
        }
        protected BaseSubArea(int id, IList<Direction> governedEdges)
            : this(governedEdges)
        {
            this.id = id;
        }
        protected BaseSubArea(IList<Direction> governedEdges)
            : this()
        {
            edges = governedEdges.ToList();
        }
        #endregion constructors

        #region indexer
        public bool this[Direction direction] => Edges.Contains(direction);
        #endregion indexer

        #region public 
        public bool CanBeAdjacent(ISubArea other)
        {
            if (AreaType != other.AreaType)
                return false;

            Direction facingMajorDirection = GetDirection(other);

            IEnumerable<Direction> minorDirections = facingMajorDirection.MinorDirections().Where(minorDirection => ActualEdges.Contains(minorDirection));

            if (!minorDirections.Any())
                return false;

            return minorDirections.All(minorDirection => other.ActualEdges.Contains(minorDirection.Opposite())) &&
                ActualEdges.Contains(facingMajorDirection) ? other.ActualEdges.Contains(facingMajorDirection.Opposite()) : true;
        }
        public void SetMeeple(Meeple meeple)
        {
            if (this.meeple != null)
                throw new AddMeepleException(this, meeple);

            this.meeple = meeple;
        }
        #endregion public

        #region private
        private Direction GetDirection(ISubArea other) => Parent.GetDirection(other.Parent);
        private int GetPoints() { throw new NotImplementedException(); }

        public bool IsAdjacent(ISubArea rhs) => Parent | rhs.Parent;

        public bool IsAdjacent(Tile rhs) => Parent | rhs;
        #endregion private

        #region factory
        private static int current;
        public static ISubArea Get(IList<Direction> directions, AreaType areaType)
        {
            int id = ++current;

            switch (areaType)
            {
                case AreaType.Castle:
                    return CastleSubArea.Get(directions);
                case AreaType.Field:
                    return FieldSubArea.Get(directions);
                case AreaType.Road:
                    return RoadSubArea.Get(directions);
                default:
                    throw new ArgumentException("No such AreaType exists");
            }
        }
        #endregion factory
    }
}