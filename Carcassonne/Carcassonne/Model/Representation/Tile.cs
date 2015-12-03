using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Representation
{
    /// <summary>
    /// Egy játékmező logikai reprezentációja, tartalmazza az absztrakt interakciós logikát.
    /// </summary>
    public class Tile
    {
        #region Declarations
        private TileSideDescriptor sideDescriptor;

        public TileSideDescriptor SideDescriptor
        {
            get { return sideDescriptor; }
            set { sideDescriptor = value; }
        }

        public Tile() { }
        #endregion Declarations
    }
    public struct TileSideDescriptor
    {
        public TileSideDescriptor(TileSideType up, TileSideType down, TileSideType left, TileSideType right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
        public TileSideType Up, Down, Left, Right;
    }

    public enum TileSideType
    {
        Field,
        Road,
        Castle
    }
}
