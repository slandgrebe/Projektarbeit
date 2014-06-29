using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    /*public class Log4netTraceListener : System.Diagnostics.TraceListener
    {
        private readonly log4net.ILog _log;

        public Log4netTraceListener()
        {
            _log = log4net.LogManager.GetLogger("System.Diagnostics.Redirection");
        }

        public Log4netTraceListener(log4net.ILog log)
        {
            _log = log;
        }

        public override void Write(string message)
        {
            if (_log != null)
            {
                _log.Debug(message);
            }
        }

        public override void WriteLine(string message)
        {
            if (_log != null)
            {
                _log.Debug(message);
            }
        }
    }*/

    /// <summary>
    /// Trace Level für log4net: http://stackoverflow.com/questions/1394661/why-isnt-there-a-trace-level-in-log4net
    /// </summary>
    public static class ILogExtentions
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Trace(this log4net.ILog log, string message, Exception exception)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                log4net.Core.Level.Trace, message, exception);
        }

        public static void Trace(this log4net.ILog log, string message)
        {
            log.Trace(message, null);
        }

        public static void Verbose(this log4net.ILog log, string message, Exception exception)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,
                log4net.Core.Level.Verbose, message, exception);
        }

        public static void Verbose(this log4net.ILog log, string message)
        {
            log.Verbose(message, null);
        }

    }
}
