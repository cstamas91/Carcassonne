using CarcassonneServer.Model.Representation.SubAreas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public abstract class BaseArea : IBaseArea
    {
        #region Meeples
        public IEnumerable<Player> Owners => meeples.MostOf(m => m.Owner);
        protected IEnumerable<Meeple> meeples => subAreas.Select(subArea => subArea.Meeple);
        public void AddMeeple(Meeple meeple, int id) =>
            subAreas.FirstOrDefault(subarea => subarea.Id == id).SetMeeple(meeple);
        #endregion Meeples

        #region Areas
        public string GUID { get; private set; }
        protected List<ISubArea> subAreas;
        public IEnumerable<ISubArea> SubAreas => subAreas;
        /// <summary>
        /// Azok a részterületek, amik teljesen körbe vannak véve.
        /// </summary>
        protected List<ISubArea> SurroundedSubAreas { get; set; }
        /// <summary>
        /// Azok a részterületek, amiknek van még olyan oldala, ami szabad.
        /// </summary>
        protected List<ISubArea> OpenSubAreas { get; set; }
        protected IEnumerable<ISubArea> GetAdjacentSubAreas(ISubArea target) => SubAreas.Where(a => a.Position | target.Position);
        protected IEnumerable<Tile> GetNeighboringTilesInArea(Tile otherTile)
        {
            throw new NotImplementedException();
        }
        protected bool IsEmpty() => subAreas.Count() == 0;
        public HashSet<Position> Positions { get; set; }
        public bool Contains(Position position) => Positions.Contains(position);
        virtual public AreaType AreaType { get; }
        virtual public bool IsFinished
        {
            get
            {
                return EvaluateIsFinished();
            }
        }
        virtual public void AddSubArea(ISubArea subArea)
        {
            if (subArea.AreaType != AreaType)
                throw new ArgumentException("Nem egyezik az alterület típusa a terület típusával.");

            if (CanAdd(subArea))
            {
                Positions.Add(subArea.Parent);
                subAreas.Add(subArea);
                SortSubAreas();
            }
            else
                throw new AddTileException(this, subArea);

            if (!Invariant())
                throw new InvariantFailedException(this, "");
        }
        virtual public bool CanAdd(ISubArea subArea)
        {
            if (subAreas.Count == 0 && AreaType == subArea.AreaType)
                return true;

            if (IsFinished || !HasAdjacentSubarea(subArea))
                return false;

            IEnumerable<ISubArea> borders = OpenSubAreas.Where(osa => osa.IsAdjacent(subArea));
            return borders.All(border => border.CanBeAdjacent(subArea));
        }
        /// <summary>
        /// Kiértékeli, hogy egy mező körbe van-e véve a területhez tartozó többi mezővel, vagy van még szabad oldala.
        /// </summary>
        /// <param name="item">A vizsgált részterület.</param>
        /// <returns>A vizsgált részterület be van-e kerítve vagy nem.</returns>
        protected bool IsSurrounded(ISubArea item)
        {
            bool isSurrounded = true;
            foreach (Direction direction in item.ActualEdges)
            {
                try
                {
                    var pos = item.Parent.GetPosition(direction);
                    isSurrounded &= 
                        Positions.Contains(pos) &&
                        subAreas.Any(subArea => (subArea.Position as Position).Equals(pos) && subArea.ActualEdges.Contains(direction.Opposite()));
                }
                catch (OutOfBoundsException oobEx)
                {
                    if (!oobEx.Position.IsBounded)
                        throw;
                    isSurrounded &= false;
                }
            }

            return isSurrounded;
        }
        virtual public void RemoveSubArea(ISubArea subArea)
        {
            if (!SubAreas.Contains(subArea))
                throw new ArgumentException("Az alterület nem része a területnek.");

            Positions.Remove(subArea.Parent);
        }
        virtual public BaseArea Merge(IBaseArea other)
        {
            if (other.AreaType != AreaType)
                throw new AreaMergeException(string.Format("Invalid types: {0} and {1}", AreaType, other.AreaType), this, other);

            var otherSubAreas = other.SubAreas.ToList();
            foreach (var item in otherSubAreas)
            {
                other.RemoveSubArea(item);
                AddSubArea(item);
            }

            return this;
        }
        virtual protected void SortSubArea(ISubArea area)
        {
            if (IsSurrounded(area))
                SurroundedSubAreas.Add(area);
            else
                OpenSubAreas.Add(area);
        }
        virtual protected void SortSubAreas()
        {
            OpenSubAreas = new List<ISubArea>();
            SurroundedSubAreas = new List<ISubArea>();

            subAreas.ForEach(SortSubArea);
        }
        virtual protected bool IsNeighbourTo(Position element) => Positions.Contains(element);
        virtual protected bool IsNeighbourTo(BaseArea area) => area.Positions.Any(p => Positions.Any(pp => pp | p));
        virtual protected bool EvaluateIsFinished() => (OpenSubAreas.Count == 0) && (SurroundedSubAreas.Count == subAreas.Count) && (subAreas.Count > 0);
        private bool Invariant() => OpenSubAreas.Count + SurroundedSubAreas.Count == subAreas.Count;
        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal terület</param>
        /// <param name="rhs">Jobb terület</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(BaseArea lhs, BaseArea rhs)
        {
            return lhs.IsNeighbourTo(rhs);
        }
        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal terület</param>
        /// <param name="rhs">Jobb Position</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(BaseArea lhs, Position rhs)
        {
            return lhs.IsNeighbourTo(rhs);
        }
        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Jobb Position</param>
        /// <param name="rhs">Bal terület</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(Position lhs, BaseArea rhs)
        {
            return rhs.IsNeighbourTo(lhs);
        }
        public bool IsAdjacent(Tile tile) => tile.GetAdjacentPositions().Any(p => Positions.Contains(p));
        private bool HasAdjacentSubarea(ISubArea subArea) => OpenSubAreas.Count > 0 && OpenSubAreas.Any(osa => osa.IsAdjacent(subArea));
        #endregion Areas

        #region Constructors
        virtual public int Score { get { return 0; } }
        protected int id;
        protected BaseArea()
        {
            GUID = Guid.NewGuid().ToString();
            subAreas = new List<ISubArea>();
            SurroundedSubAreas = new List<ISubArea>();
            OpenSubAreas = new List<ISubArea>();
            Positions = new HashSet<Position>();
        }
        #endregion Constructors

        #region factory
        public static IBaseArea Get(ISubArea initialArea)
        {
            switch (initialArea.AreaType)
            {
                case AreaType.Castle:
                    return CastleArea.Get(initialArea);
                case AreaType.Field:
                    return FieldArea.Get(initialArea);
                case AreaType.Road:
                    return RoadArea.Get(initialArea);
                default:
                    throw new ArgumentException();
            }
        }
        #endregion factory
    }
}
