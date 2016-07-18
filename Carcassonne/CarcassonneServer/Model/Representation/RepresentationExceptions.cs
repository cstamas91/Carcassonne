using System;
using CarcassonneSharedModules.Logger;
using System.Runtime.CompilerServices;

namespace CarcassonneServer.Model.Representation
{
    public abstract class AutoLoggingException : Exception
    {
        public AutoLoggingException(string message, [CallerMemberName] string callerName = "")
            :base(message)
        {
            Logger.WriteLog(string.Format("{0}: {1}", callerName, message));
        }
    }

    public class UnrealisticResultException : AutoLoggingException
    {
        public UnrealisticResultException(string Message)
            :base(Message)
        {
            
        }
    }
}
