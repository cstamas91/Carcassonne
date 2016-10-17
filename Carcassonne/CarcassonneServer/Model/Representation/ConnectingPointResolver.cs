using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneServer.Model.Representation
{
    public static class ConnectingPointResolver
    {
        public static ConnectingPoint ResolveOpposite(this ConnectingPoint instance)
        {
            switch (instance)
            {
                case ConnectingPoint.Up:
                    return ConnectingPoint.Down;
                case ConnectingPoint.UpRight:
                    return ConnectingPoint.DownRight;
                case ConnectingPoint.RightUp:
                    return ConnectingPoint.LeftUp;
                case ConnectingPoint.Right:
                    return ConnectingPoint.Left;
                case ConnectingPoint.RightDown:
                    return ConnectingPoint.LeftDown;
                case ConnectingPoint.DownRight:
                    return ConnectingPoint.UpRight;
                case ConnectingPoint.Down:
                    return ConnectingPoint.Up;
                case ConnectingPoint.DownLeft:
                    return ConnectingPoint.UpLeft;
                case ConnectingPoint.LeftDown:
                    return ConnectingPoint.RightDown;
                case ConnectingPoint.Left:
                    return ConnectingPoint.Right;
                case ConnectingPoint.LeftUp:
                    return ConnectingPoint.RightUp;
                case ConnectingPoint.UpLeft:
                default:
                    return ConnectingPoint.DownLeft;
            }
        }
    }
}
