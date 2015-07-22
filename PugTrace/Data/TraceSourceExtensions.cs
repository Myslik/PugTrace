using System;
using System.Diagnostics;
using System.Web;

namespace PugTrace.Data
{
    public static class TraceSourceExtensions
    {
        public static void TraceException(this TraceSource source, int id, Exception exception)
        {
            var data = new DataCollector().Add(exception).ToData();
            source.TraceData(TraceEventType.Error, id, data);
        }

        public static void TraceWebException(this TraceSource source, int id, HttpContextBase context, Exception exception)
        {
            var data = new DataCollector().Add(context).Add(exception).ToData();
            source.TraceData(TraceEventType.Error, id, data);
        }
    }
}