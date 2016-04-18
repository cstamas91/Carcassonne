using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarcassonneSharedModules.Logger;

namespace CarcassonneServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.LogToConsole = true;
            Logger.WriteLog("Server started.");
            Logger.WriteLog("Server stopping.");
            Logger.EndLog();
        }
    }
}
