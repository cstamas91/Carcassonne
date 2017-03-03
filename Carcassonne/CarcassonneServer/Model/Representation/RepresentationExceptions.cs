using System;
using CarcassonneSharedModules.Logger;
using System.Runtime.CompilerServices;
using CarcassonneServer.Model.Representation.Area;
using System.Linq;
using CarcassonneServer.Model.Representation.SubAreas;

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
            : base(Message, callerName) { }
    }

    public class InvalidStateException : AutoLoggingException
    {
        public InvalidStateException(string message, [CallerMemberName] string callerName = "")
            : base(message, callerName) { }
    }

    public class OutOfBoundsException : AutoLoggingException
    {
        private Position position;
        public Position Position => position;
        private Direction direction;
        public Direction Direction => direction;

        public OutOfBoundsException(string message, [CallerMemberName] string callerName = "")
            : base(message, callerName)
        { }

        public OutOfBoundsException(string message, Position position, Direction direction, [CallerMemberName] string callerName = "")
            : this (message, callerName)
        {
            this.position = position;
            this.direction = direction;
        }
    }

    public class AddTileException : AutoLoggingException
    {
        public AddTileException(string message, [CallerMemberName] string callerName = "")
         : base(message, callerName){ }

        public AddTileException(IBaseArea area, ISubArea subArea, [CallerMemberName] string callerName = "")
            : base (string.Format("Positions: {0}, AreaType: {1}, Position: {2}, Type: {3}",
                    area.Positions.Aggregate("", (str, pos) => str += string.Format("{0}\n\r", pos.ToString())),
                    subArea.AreaType.ToString(),
                    subArea.Position.ToString(),
                    subArea.AreaType.ToString()), callerName) { }
    }

    public class AddMeepleException : AutoLoggingException
    {
        public AddMeepleException(string message, [CallerMemberName] string callerName = "")
            : base(message, callerName) { }

        public AddMeepleException(ISubArea subArea, Meeple meeple, [CallerMemberName] string callerName = "")
            : base(string.Format("SubArea: {0}, Meeple: {1}", subArea.ToString(), meeple.ToString()), callerName) { }
    }

    public class InvariantFailedException : AutoLoggingException
    {
        private BaseArea area;
        public BaseArea Area => area;

        public InvariantFailedException(BaseArea area, string message, [CallerMemberName] string callerName = "")
            : base(message, callerName)
        {
            this.area = area;
        }
    }

    public class AreaMergeException : AutoLoggingException
    {
        private IBaseArea target;
        private IBaseArea other;

        public IBaseArea Target => target;
        public IBaseArea Other => other;

        public AreaMergeException(
            string message, 
            IBaseArea target,
            IBaseArea other,
            [CallerMemberName] string callerName = "")
            : base(message, callerName)
        {
            this.target = target;
            this.other = other;
        }
    }
}
