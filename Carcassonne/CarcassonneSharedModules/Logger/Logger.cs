using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarcassonneSharedModules.Logger
{
    public static class Logger
    {
        private static FileLogger fileLogger;
        private static ConsoleLogger consoleLogger;
        public static bool LogToConsole
        {
            get;
            set;
        }

        static Logger()
        {
            fileLogger = new FileLogger();
            consoleLogger = new ConsoleLogger();
        }

        public static void WriteLog(string message)
        {
            fileLogger.WriteLog(message);
            if (LogToConsole)
                consoleLogger.WriteLog(message);
        }

        public static Task EndLog()
        {
            return Task.WhenAll(fileLogger.EndLog(), consoleLogger.EndLog());
        }
    }

    public abstract class AbstractLogger
    {
        protected virtual TextWriter LogWriter { get { return null; } }

        public virtual void WriteLog(string message)
        {
            LogWriter.WriteLine(string.Format("{0}|{1}", DateTime.Now.ToString("hh:mm:ss.fff"), message));
        }
    }

    public class FileLogger : AbstractLogger
    {
        private DateTime logStartTime;

        private Stream logStream = null;
        private TextWriter logWriter = null;
        protected override TextWriter LogWriter
        {
            get
            {
                if (logWriter == null)
                {
                    if (logStream == null)
                        logStream = File.Create(BuildLogfileName());

                    logWriter = new StreamWriter(logStream);
                }

                return logWriter;
            }
        }

        protected static string LogPath
        {
            get
            {
                var path = string.Format(@"{0}\Carcassonne\Logs\", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
            set { }
        }

        public FileLogger()
        {
            logStartTime = DateTime.Now;
        }

        public async Task EndLog()
        {
            try
            {
                var str = BuildLogfileName();
                using (var logFile = File.Create(str))
                {
                    await logStream.CopyToAsync(logFile);
                }
            }
            catch(Exception e)
            {

            }
            finally
            {
                logWriter.Dispose();
            }
        }

        private string BuildLogfileName()
        {
            return string.Format(@"{0}{1}__{2}.log", LogPath, DateTime.Today.ToString("yyyy-MM-dd"), logStartTime.ToString("hh-mm-ss-fff"));
        }
    }

    public class ConsoleLogger : AbstractLogger
    {
        protected override TextWriter LogWriter { get { return Console.Out; } }

        public ConsoleLogger() { }
        
        public Task EndLog()
        {
            return Task.FromResult(true);
        }
    }
}
