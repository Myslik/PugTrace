using System.Configuration;
using System.Data.SqlClient;

namespace PugTrace.SqlServer
{
    public class SqlServerTraceStorage : TraceStorage
    {
        private string ConnectionString { get; set; }

        public SqlServerTraceStorage(string nameOrConnectionString)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[nameOrConnectionString];
            if (connectionStringSettings == null)
            {
                ConnectionString = nameOrConnectionString;
            }
            else
            {
                ConnectionString = connectionStringSettings.ConnectionString;
            }
        }

        public override Storage.IStorageConnection GetConnection()
        {
            var connection = CreateAndOpenConnection();
            return new SqlServerConnection(connection);
        }

        internal SqlConnection CreateAndOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}