using FligthNode.Service.App;
using log4net;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;

namespace FlightNode.Service.App
{
    public class Log4NetTracer : ITraceWriter
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Log4NetTracer));

        public TraceLevel LevelFilter
        {
            get { return TraceLevel.Warning; }
        }

        public void Trace(TraceLevel level, string message, Exception ex)
        {
            LogEventInfo logEvent = new LogEventInfo
            {
                Message = message,
                Exception = ex
            };

            switch (level)
            {
                case TraceLevel.Error:
                    Logger.Error(logEvent);
                    return;
                case TraceLevel.Warning:
                    Logger.Warn(logEvent);
                    return;
                case TraceLevel.Info:
                    Logger.Info(logEvent);
                    return;
                case TraceLevel.Off:
                    // do nothing
                    return;
                default:
                    Logger.Debug(logEvent);
                    return;
            }
        }
    }
}