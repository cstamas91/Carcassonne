using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public abstract class BaseConstruction
    {
        protected List<Tile> elements = new List<Tile>();
        protected List<Meeple> meeples = new List<Meeple>();
        public int Size { get { return elements.Count; } }

        public BaseConstruction()
        {
            this.GUID = Guid.NewGuid().ToString();
        }

        virtual public TileSideType AreaType { get; }
        virtual public bool IsFinished { get; }
        virtual public IEnumerable<Tile> EdgeTiles { get; }
        public string GUID { get; private set; }

        virtual public void AddElement(Tile element) { }
        virtual public void AddMeeple(Meeple meeple) { }

        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal Construction</param>
        /// <param name="rhs">Jobb Construction</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(BaseConstruction lhs, BaseConstruction rhs)
        {
            return lhs.NeighbourTo(rhs);
        }
        /// <summary>
        /// Operátor túltöltés szomszédsági kapcsolat eldöntéséhez.
        /// </summary>
        /// <param name="lhs">Bal Construction</param>
        /// <param name="rhs">Jobb Tile</param>
        /// <returns>Igazat, ha Bal és Jobb szomszédok, egyébként hamisat.</returns>
        public static bool operator |(BaseConstruction lhs, Position rhs)
        {
            return lhs.NeighbourTo(rhs);
        }

        public static bool operator |(Position lhs, BaseConstruction rhs)
        {
            return rhs.NeighbourTo(lhs);
        }

        virtual public BaseConstruction Merge(BaseConstruction other) { return null; }
        virtual protected bool NeighbourTo(Position element) { return false; }
        virtual protected bool NeighbourTo(BaseConstruction construction) { return false; }
        virtual protected bool EvaluateIsFinished() { return false; }
    }
}
