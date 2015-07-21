using System;
using System.ComponentModel;

namespace PugTrace.SqlServer
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class GlobalConfigurationExtensions
    {
        public static IGlobalConfiguration<SqlServerTraceStorage> UseSqlServerStorage(
            this IGlobalConfiguration configuration,
            string nameOrConnectionString)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (nameOrConnectionString == null) throw new ArgumentNullException("nameOrConnectionString");

            SqlServerObjectsInstaller.Install(nameOrConnectionString);
            return configuration.UseStorage<SqlServerTraceStorage>(new SqlServerTraceStorage(nameOrConnectionString));
        }
    }
}
