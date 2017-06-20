using System;
using NLog;

namespace WebApplication1.Codes
{
    public class OperationLog
    {
        readonly static Logger operLogger = LogManager.GetLogger("operlogger");

        public static void ProcessInfo(string operUserName, string operIp, string content)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, "", content);
            theEvent.Properties["OperUser"] = operUserName;
            theEvent.Properties["IpAddress"] = operIp;
            operLogger.Log(theEvent);
        }

        public static void ProcessError(string operUserName, string operIp, string content,Exception ex)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Error, "", null, content, null, ex);
            theEvent.Properties["OperUser"] = operUserName;
            theEvent.Properties["IpAddress"] = operIp;
            operLogger.Log(theEvent);
        }

        public static void Flush()
        {
            LogManager.Flush();
        }
    }
}
