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

        public IEnumerable<TraceData> Get(int take = 20)
        {
            const string sql = @"select top (@top) * from dbo.diagnostics_Trace order by UtcDateTime desc";

            return _connection.Query<TraceData>(sql, new { top = take });
        }

        public TraceData Get(string id)
        {
            if (id == null) throw new ArgumentNullException("id");

            const string sql = @"select * from dbo.diagnostics_Trace where TraceId = @id";

            return _connection.Query<TraceData>(sql, new { id = id }).SingleOrDefault();
        }

        public virtual void Dispose()
        {
            _connection.Dispose();
        }
    }
}