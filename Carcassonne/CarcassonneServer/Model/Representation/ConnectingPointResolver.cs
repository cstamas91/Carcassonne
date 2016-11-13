using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation
{
    public static class ConnectingPointResolver
    {
        public static Direction ResolveOpposite(this Direction instance)
        {
            switch (instance)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.UpRight:
                    return Direction.DownRight;
                case Direction.RightUp:
                    return Direction.LeftUp;
                case Direction.Right:
                    return Direction.Left;
                case Direction.RightDown:
                    return Direction.LeftDown;
                case Direction.DownRight:
                    return Direction.UpRight;
                case Direction.Down:
                    return Direction.Up;
                case Direction.DownLeft:
                    return Direction.UpLeft;
                case Direction.LeftDown:
                    return Direction.RightDown;
                case Direction.Left:
                    return Direction.Right;
                case Direction.LeftUp:
                    return Direction.RightUp;
                case Direction.UpLeft:
                default:
                    return Direction.DownLeft;
            }
        }
    }
}
