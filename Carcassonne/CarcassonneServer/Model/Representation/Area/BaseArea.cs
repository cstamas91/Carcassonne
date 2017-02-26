using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public abstract class BaseArea : IBaseArea
    {
        #region Meeples
        public IEnumerable<Player> Owners
        {
            get
            {
                return meeples.MostOf(m => m.Owner);
            }
        }
        protected List<Meeple> meeples = new List<Meeple>();
        virtual public void AddMeeple(Meeple meeple, SubArea subArea) { }
        #endregion Meeples

        #region Scoring
        virtual public int Score { get; }
        #endregion Scoring

        #region Areas
        public string GUID { get; private set; }
        public IEnumerable<SubArea> SubAreas { get { return subAreas; } }
        /// <summary>
        /// Azok a részterületek, amik teljesen körbe vannak véve.
        /// </summary>
        protected List<SubArea> SurroundedSubAreas { get; set; }
        /// <summary>
        /// Azok a részterületek, amiknek van még olyan oldala, ami szabad.
        /// </summary>
        protected List<SubArea> OpenSubAreas { get; set; }
        protected List<SubArea> subAreas;
        protected IEnumerable<SubArea> GetAdjacentSubAreas(SubArea target)
        {
            IEnumerable<Direction> directions = target.Edges;

            return SubAreas.Where(a => a.Position | target.Position);
        }
        protected IEnumerable<Tile> GetNeighboringTilesInArea(Tile otherTile)
        {
            throw new NotImplementedException();
        }
        protected bool IsEmpty()
        {
            return this.subAreas.Count() == 0;
        }
        public HashSet<Position> Positions { get; set; }

        virtual public AreaType AreaType { get; }
        virtual public bool IsFinished
        {
            get
            {
                return EvaluateIsFinished();
            }
        }
        virtual public void AddSubArea(SubArea subArea)
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
                throw new TileAddException(this, subArea);

            if (!Invariant())
                throw new InvariantFailedException(this, "");
        }
        protected abstract bool CanAdd(SubArea subArea);
        /// <summary>
        /// Kiértékeli, hogy egy mező körbe van-e véve a területhez tartozó többi mezővel, vagy van még szabad oldala.
        /// </summary>
        /// <param name="item">A vizsgált részterület.</param>
        /// <returns>A vizsgált részterület be van-e kerítve vagy nem.</returns>
        protected bool IsSurrounded(SubArea item)
        {
            bool isSurrounded = true;
            foreach (Direction direction in item.Edges.Select(d => d.GetTileDirectionFromAreaDirection(item.Parent.Rotation)))
            {
                try
                {
                    isSurrounded &= Positions.Contains(item.Parent.GetPosition(direction));
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

        virtual public void RemoveSubArea(SubArea subArea)
        {
            if (!SubAreas.Contains(subArea))
                throw new ArgumentException("Az alterület nem része a területnek.");

            Positions.Remove(subArea.Parent);
        }
        virtual public BaseArea Merge(BaseArea other)
        {
            var otherSubAreas = other.SubAreas.ToList();
            foreach (var item in otherSubAreas)
            {
                other.RemoveSubArea(item);
                AddSubArea(item);
            }

            return this;
        }
        virtual protected void SortSubArea(SubArea area)
        {
            if (IsSurrounded(area))
                SurroundedSubAreas.Add(area);
            else
                OpenSubAreas.Add(area);
        }
        virtual protected void SortSubAreas()
        {
            OpenSubAreas = new List<SubArea>();
            SurroundedSubAreas = new List<SubArea>();

            subAreas.ForEach(SortSubArea);
        }
        virtual protected bool IsNeighbourTo(Position element)
        {
            return Positions.Contains(element);
        }
        virtual protected bool IsNeighbourTo(BaseArea area)
        {
            return area.Positions.Any(p => Positions.Any(pp => pp | p));
        }
        virtual protected bool EvaluateIsFinished()
        {
            return OpenSubAreas.Count == 0 && SurroundedSubAreas.Count == subAreas.Count;
        }
        bool Invariant()
        {
            return OpenSubAreas.Count + SurroundedSubAreas.Count == subAreas.Count;
        }
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
        #endregion Areas

        #region Constructors
        public BaseArea()
        {
            this.GUID = Guid.NewGuid().ToString();
            subAreas = new List<SubArea>();
            SurroundedSubAreas = new List<SubArea>();
            OpenSubAreas = new List<SubArea>();
            Positions = new HashSet<Position>();
        }
        #endregion Constructors
    }
}
