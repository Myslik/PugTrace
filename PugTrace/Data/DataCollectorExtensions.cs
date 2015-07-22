using System;
using System.Web;

namespace PugTrace.Data
{
    public static class DataCollectorExtensions
    {
        public static DataCollector Add(this DataCollector collector, Exception exception)
        {
            collector.Add(new ExceptionData(exception));
            return collector;
        }

        public static DataCollector Add(this DataCollector collector, HttpContextBase context)
        {
            collector.Add(new RequestData(context));
            return collector;
        }
    }
}