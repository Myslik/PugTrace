using Dapper;
using PugTrace.Storage;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PugTrace.SqlServer.Queries
{
    internal static class SqlServerQueries
    {
        private static string _sqlGetTraces = GetScript("GetTraces");
        private static string _sqlGetTraceDetails = GetScript("GetTraceDetails");
        private static string _sqlGetTraceCount = GetScript("GetTraceCount");

        private static string GetScript(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(string.Format("{0}.{1}.{2}", typeof(SqlServerQueries).Namespace, scriptName, "sql"));
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        internal static IEnumerable<TraceData> GetTraces(this SqlConnection connection, int skip, int top, string type = null)
        {
            return connection.Query<TraceData>(_sqlGetTraces, new { top = top, skip = skip, type = type });
        }

        internal static TraceData GetTraceDetails(this SqlConnection connection, int id)
        {
            return connection.Query<TraceData>(_sqlGetTraceDetails, new { id = id }).SingleOrDefault();
        }

        internal static int GetTraceCount(this SqlConnection connection, string type)
        {
            return connection.ExecuteScalar<int>(_sqlGetTraceCount, new { type = type });
        }
    }
}
