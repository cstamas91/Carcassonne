using System;
using CarcassonneSharedModules.Logger;
using System.Runtime.CompilerServices;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;

namespace CarcassonneServer.Model.Representation
{
    [Serializable]
    public abstract class AutoLoggingException : Exception
    {
        protected string ExceptionLogStringSchema = "{0} : {1}";
        public AutoLoggingException(string message, [CallerMemberName] string callerName = "")
            : base(message)
        {
            Logger.WriteLog(string.Format(ExceptionLogStringSchema, callerName, message));
        }
    }
    [Serializable]
    public class UnrealisticResultException : AutoLoggingException
    {
        public UnrealisticResultException(string Message, [CallerMemberName] string callerName = "")
            : base(Message, callerName)
        {

        }
    }

    public class InvalidStateException : AutoLoggingException
    {
        public InvalidStateException(string message, [CallerMemberName] string callerName = "")
            : base(message, callerName)
        {
        }
    }

    public class OutOfBoundsException : AutoLoggingException
    {
        public Position Position { get; set; }
        public Direction Direction { get; set; }

        public OutOfBoundsException(string message, [CallerMemberName] string callerName = "")
            : base(message, callerName)
        { }

        public OutOfBoundsException(string message, Position position, Direction direction, [CallerMemberName] string callerName = "")
            : this (message, callerName)
        {
            Position = position;
            Direction = direction;
        }
    }

    public class TileAddException : AutoLoggingException
    {
        public TileAddException(string message, [CallerMemberName] string callerName = "")
         : base(message, callerName){ }

        public TileAddException(BaseArea area, SubArea subArea, [CallerMemberName] string callerName = "")
            : base (string.Format("Positions: {0}, AreaType: {1}, Position: {2}, Type: {3}",
                    area.Positions.Aggregate("", (str, pos) => str += string.Format("{0}\n\r", pos.ToString())),
                    subArea.AreaType.ToString(),
                    subArea.Position.ToString(),
                    subArea.AreaType.ToString()), callerName) { }
    }

    public class InvariantFailedException : AutoLoggingException
    {
        private BaseArea area;
        public BaseArea Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
            }
        }

        public InvariantFailedException(BaseArea area, string message, [CallerMemberName] string callerName = "")
            : base(message, callerName)
        {
            Area = area;
        }
    }
}
