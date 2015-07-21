using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace PugTrace.SqlServer
{
    public static class SqlServerObjectsInstaller
    {
        public static void Install(string nameOrConnectionString)
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[nameOrConnectionString];
            if (connectionStringSettings == null) {
                RunScript("Installer", nameOrConnectionString);
            } else {
                RunScript("Installer", connectionStringSettings.ConnectionString);
            }
        }

        private static void RunScript(string scriptName, string connectionString)
        {
            var script = GetScript(scriptName);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(null, connection);
                var reader = new StringReader(script);
                var line = string.Empty;
                var section = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line == null || string.Equals(line.Trim(), "GO", StringComparison.OrdinalIgnoreCase))
                    {
                        if (section.Length > 0)
                        {
                            command.CommandText = section.ToString();
                            command.ExecuteNonQuery();

                            section = new StringBuilder();
                        }
                    }
                    else
                    {
                        section.AppendLine(line);
                    }
                }
            }
        }

        private static string GetScript(string scriptName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(string.Format("{0}.{1}.{2}", typeof(SqlServerObjectsInstaller).Namespace, scriptName, "sql"));
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
