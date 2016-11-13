using System;
using CarcassonneSharedModules.Logger;
using System.Runtime.CompilerServices;

namespace CarcassonneServer.Model.Representation
{
    [Serializable]
    public abstract class AutoLoggingException : Exception
    {
        protected string ExceptionLogStringSchema = "{0} : {1}";
        public AutoLoggingException(string message, [CallerMemberName] string callerName = "")
            :base(message)
        {
            Logger.WriteLog(ExceptionLogStringSchema, callerName, message));
        }
    }
    [Serializable]
    public class UnrealisticResultException : AutoLoggingException
    {
        public UnrealisticResultException(string Message, [CallerMemberName] string callerName = "")
            :base(Message, callerName)
        {
            
        }
    }
    [Serializable]
    public class TileMatchException : AutoLoggingException
    {
        Tile tile1;
        Tile tile2;
        public TileMatchException(string message, Tile tile1, Tile tile2, [CallerMemberName] string callerName = "")
            :base(message, callerName)
        {
            this.tile1 = tile1;
            this.tile2 = tile2;
        }

        TileSideDescriptor[] OppositeSides
        {
            get
            {
                TileSideDescriptor[] sides = new TileSideDescriptor[2];
                sides[0] = tile1[tile1.AdjacentDirection(tile2)];
                sides[1] = tile2[tile2.AdjacentDirection(tile1)];

                return sides;
            }
        } 
    }

    public class InvalidStateException : AutoLoggingException
    {
        public InvalidStateException(string message, [CallerMemberName] string callerName = "")
            : base (message, callerName)
        {
        }
    }
}
