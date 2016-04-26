using System;
using System.Collections.Generic;
using System.Linq;

namespace CarcassonneServer.Model.Representation.Construction
{
    public abstract class BaseConstruction
    {
        protected List<Tile> elements = new List<Tile>();
        public int Size { get { return elements.Count; } }

        public BaseConstruction() { }
        
        virtual public TileSideType AreaType { get; }
        virtual public bool IsFinished { get; }
        virtual public List<Tile> EdgeTiles { get; }
        
        virtual public void AddElement(Tile element) { }

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
        public static bool operator |(BaseConstruction lhs, Tile rhs)
        {
            return lhs.NeighbourTo(rhs);
        }
        virtual protected bool NeighbourTo(Tile element) { return false; }
        virtual protected bool NeighbourTo(BaseConstruction construction) { return false; }
        virtual protected bool EvaluateIsFinished() { return false; }
    }
}
