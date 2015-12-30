using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Model.Representation
{
    #region Tile
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

    public enum TileFieldType
    {
        Road,
        Field,
        Castle,
        Monastery
    }
    #endregion Tile
}
