using System;
using CarcassonneSharedModules.Logger;
using System.Runtime.CompilerServices;

namespace CarcassonneServer.Model.Representation
{
    [Serializable]
    public abstract class AutoLoggingException : Exception
    {
        public AutoLoggingException(string message, [CallerMemberName] string callerName = "")
            :base(message)
        {
            Logger.WriteLog(string.Format("{0}: {1}", callerName, message));
        }
    }
    [Serializable]
    public class UnrealisticResultException : AutoLoggingException
    {
        public UnrealisticResultException(string Message)
            :base(Message)
        {
            
        }
    }
    [Serializable]
    public class TileMatchException : AutoLoggingException
    {
        Tile tile1;
        Tile tile2;
        public TileMatchException(string message, Tile tile1, Tile tile2)
            :base(message)
        {
            this.tile1 = tile1;
            this.tile2 = tile2;
        }

        TileSideDescriptor[] OppositeSides
        {
            get
            {
                TileSideDescriptor[] sides = new TileSideDescriptor[2];
                sides[0] = tile1[tile1.NeighborDirection(tile2)];
                sides[1] = tile2[tile2.NeighborDirection(tile1)];

                return sides;
            }
        } 
    }
}
