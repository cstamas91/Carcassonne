using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public abstract class BaseConstruction : IBaseConstruction
    {
        protected Dictionary<TileTag, ICollection<Tile>> elements = new Dictionary<TileTag, ICollection<Tile>>()
        {
            { TileTag.Border, new List<Tile>() },
            { TileTag.Edge, new List<Tile>() },
            { TileTag.Inner, new List<Tile>() }
        };
        protected List<Meeple> meeples = new List<Meeple>();
        public int Size { get { return elements.Values.Sum(item => item.Count); } }
        public short Score { get; protected set; }

        public BaseConstruction()
        {
            this.GUID = Guid.NewGuid().ToString();
        }

        virtual public TileSideType AreaType { get; }
        virtual public bool IsFinished { get; }
        public IEnumerable<Tile> EdgeTiles
        {
            get
            {
                if (!elements.ContainsKey(TileTag.Edge))
                    elements.Add(TileTag.Edge, new List<Tile>());

                return elements[TileTag.Edge];
            }
        }
        public string GUID { get; private set; }

        virtual public void AddElement(ref Tile element) { }
        virtual public void AddMeeple(Meeple meeple) { }

        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal Construction</param>
        /// <param name="rhs">Jobb Construction</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(BaseConstruction lhs, BaseConstruction rhs)
        {
            return lhs.IsNeighbourTo(rhs);
        }
        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal Construction</param>
        /// <param name="rhs">Jobb Position</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(BaseConstruction lhs, Position rhs)
        {
            return lhs.IsNeighbourTo(rhs);
        }
        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Jobb Position</param>
        /// <param name="rhs">Bal Construction</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(Position lhs, BaseConstruction rhs)
        {
            return rhs.IsNeighbourTo(lhs);
        }

        virtual public BaseConstruction Merge(BaseConstruction other)
        {
            return null;
        }
        virtual public Direction NeighborDirection(Position other) { throw new NotImplementedException(); }
        virtual protected bool IsNeighbourTo(Position element) { return false; }
        virtual protected bool IsNeighbourTo(BaseConstruction construction) { return false; }
        virtual protected bool EvaluateIsFinished() { return false; }

        /// <summary>
        /// Mezők címkézésére szolgáló enum.
        /// </summary>
        public enum TileTag
        {
            Inner, //mind a 4 oldalán szomszédos egy azonos építménybeli mezővel
            Edge, //van olyan oldala, ami nem szomszédos semmilyen mezővel
            Border //azok az oldalai, amik az építménnyel szomszédosak, "lezáró" oldalak (várfal)
        }
    }
}
