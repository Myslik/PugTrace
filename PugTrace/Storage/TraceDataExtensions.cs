using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PugTrace.Data;
using System;

namespace PugTrace.Storage
{
    public static class TraceDataExtensions
    {
        public static ExceptionData GetException(this TraceData trace)
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
                    var exceptionData = jToken.ToObject<ExceptionData>();
                    if (exceptionData != null && exceptionData.IsValid())
                    {
                        return exceptionData;
                    }
                }
            }
            return null;
        }
    }
}