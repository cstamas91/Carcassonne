using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Area
{
    public abstract class BaseArea : IBaseArea
    {
        #region Meeples
        protected List<Meeple> meeples = new List<Meeple>();
        virtual public void AddMeeple(Meeple meeple, SubArea subArea) { }
        #endregion Meeples

        #region Scoring
        public short Score { get; protected set; }
        #endregion Scoring

        #region Areas
        public string GUID { get; private set; }

        /// <summary>
        /// Azok a részterületek, amik teljesen körbe vannak véve.
        /// </summary>
        protected List<SubArea> SurroundedSubAreas { get; set; }
        /// <summary>
        /// Azok a részterületek, amiknek van még olyan oldala, ami szabad.
        /// </summary>
        protected List<SubArea> OpenSubAreas { get; set; }
        protected List<SubArea> subAreas;
        public IEnumerable<SubArea> SubAreas { get { return subAreas; } }
        virtual public AreaType AreaType { get; }
        virtual public bool IsFinished { get; }

        virtual public void AddSubArea(SubArea subArea)
        {
            if (subArea.AreaType != AreaType)
                throw new ArgumentException("Nem egyezik az alterület típusa a terület típusával.");
        }
        virtual public BaseArea Merge(BaseArea other)
        {
            return null;
        }
        virtual public Direction NeighborDirection(Position other) { throw new NotImplementedException(); }
        virtual protected bool IsNeighbourTo(Position element) { return false; }
        virtual protected bool IsNeighbourTo(BaseArea area) { return false; }
        virtual protected bool EvaluateIsFinished() { return false; }
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
        }
        #endregion Constructors

        
        
    }
}
