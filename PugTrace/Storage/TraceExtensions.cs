using Newtonsoft.Json.Linq;
using PugTrace.Data;

namespace PugTrace.Storage
{
    public static class TraceExtensions
    {
        public static TraceData GetData(this Trace trace)
        {
            JArray jsonArray = null;

            if (!string.IsNullOrEmpty(trace.Data))
            {
                jsonArray = JArray.Parse(trace.Data);
            }

            if (jsonArray != null)
            {
                foreach (var jToken in jsonArray)
                {
                    var traceData = jToken.ToObject<TraceData>();
                    if (traceData != null && traceData.IsValid())
                    {
                        return traceData;
                    }
                }
            }
            return null;
        }

        public static string GetMessage(this Trace trace)
        {
            var data = trace.GetData();
            if (data == null)
            {
                return trace.Message;
            }
            else
            {
                return data.ToHTML();
            }
        }
    }
}