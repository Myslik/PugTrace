using Dapper;
using PugTrace.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PugTrace.SqlServer
{
    public class SqlServerConnection : IStorageConnection
    {
        private readonly SqlConnection _connection;

        public SqlServerConnection(SqlConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            this._connection = connection;
        }

        public SqlConnection Connection { get { return _connection; } }

        public IEnumerable<TraceData> Get(int skip = 0, int top = 20)
        {
            const string sql = @"select * from (select ROW_NUMBER() over(order by [UtcDateTime] desc) as NUMBER, * from [PugTrace].[Trace]) as tbl where NUMBER between (@skip + 1) and (@top + @skip) order by [UtcDateTime] desc";

            return _connection.Query<TraceData>(sql, new { top = top, skip = skip });
        }

        public TraceData GetTraceDetail(int id)
        {
            const string sql = @"select * from [PugTrace].[Trace] where TraceId = @id";

            return _connection.Query<TraceData>(sql, new { id = id }).SingleOrDefault();
        }

        public virtual void Dispose()
        {
            _connection.Dispose();
        }

        public int Count()
        {
            return _connection.ExecuteScalar<int>("select count(*) from [PugTrace].[Trace]");
        }
    }
}