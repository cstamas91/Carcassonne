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
}
