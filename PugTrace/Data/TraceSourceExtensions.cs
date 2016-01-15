using System;
using System.Diagnostics;

namespace PugTrace.Data
{
    public static class TraceSourceExtensions
    {
        public static void PugTraceException(this TraceSource source, Exception exception)
        {
            var data = new TraceData("{ExceptionName} caught: {ExceptionMessage}", new
            {
                ExceptionName = exception.GetType().Name,
                ExceptionMessage = exception.Message
            });
            data.StackTrace = exception.StackTrace;
            source.TraceData(TraceEventType.Error, (int)TraceEventType.Error, data);
        }

        public static void PugTrace(this TraceSource source, TraceEventType eventType, string message)
        {
            var data = new TraceData(message, null);
            data.StackTrace = Environment.StackTrace;
            source.TraceData(eventType, (int)eventType, data);
        }

        public static void PugTrace(this TraceSource source, TraceEventType eventType, string format, object parameters)
        {
            var data = new TraceData(format, parameters);
            data.StackTrace = Environment.StackTrace;
            source.TraceData(eventType, (int)eventType, data);
        }
    }
}