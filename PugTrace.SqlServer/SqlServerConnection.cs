using PugTrace.SqlServer.Queries;
using PugTrace.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public IEnumerable<TraceData> Get(int skip = 0, int top = 20, string typeFilter = null)
        {
            return _connection.GetTraces(skip, top, typeFilter);
        }

        public TraceData GetTraceDetail(int id)
        {
            return _connection.GetTraceDetails(id);
        }

        public virtual void Dispose()
        {
            _connection.Dispose();
        }

        public int Count(string typeFilter = null)
        {
            return _connection.GetTraceCount(typeFilter);
        }
    }
}