using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;

namespace PugTrace.SqlServer
{
    public class SqlServerTraceListener : TraceListenerBase
    {
        //public const string DefaultTable = "diagnostics_Trace";
        string _connectionName;
        const string _defaultApplicationName = "";
        const string _defaultCommandText = "IF object_id('diagnostics_Trace_AddEntry') IS NOT NULL " +
           "EXEC diagnostics_Trace_AddEntry " +
           "@ApplicationName, @Source, @Id, @EventType, @UtcDateTime, " +
           "@MachineName, @AppDomainFriendlyName, @ProcessId, @ThreadName, " +
           "@Message, @ActivityId, @RelatedActivityId, @LogicalOperationStack, " +
           "@Data;";
        const int _defaultMaxMessageLength = 1500;

        private static string[] _supportedAttributes = new string[] 
            { 
                "applicationName", "ApplicationName", "applicationname", 
                "commandText", "CommandText", "commandtext", 
                "maxMessageLength", "MaxMessageLength", "maxmessagelength", 
            };

        public SqlServerTraceListener(string connectionName)
        {
            _connectionName = connectionName;
        }

        public string ApplicationName
        {
            get
            {
                if (Attributes.ContainsKey("applicationname"))
                {
                    return Attributes["applicationname"];
                }
                else
                {
                    return _defaultApplicationName;
                }
            }
            set
            {
                Attributes["applicationname"] = value;
            }
        }

        public string CommandText
        {
            get
            {
                if (Attributes.ContainsKey("commandtext"))
                {
                    return Attributes["commandtext"];
                }
                else
                {
                    return _defaultCommandText;
                }
            }
            set
            {
                Attributes["commandtext"] = value;
            }
        }

        public int MaxMessageLength
        {
            get
            {
                if (Attributes.ContainsKey("maxmessagelength"))
                {
                    int value;
                    if (!int.TryParse(Attributes["maxmessagelength"], out value))
                    {
                        value = _defaultMaxMessageLength;
                    }
                    return value;
                }
                else
                {
                    return _defaultMaxMessageLength;
                }
            }
            set
            {
                Attributes["maxmessagelength"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string ConnectionName
        {
            get { return _connectionName; }
        }

        protected override string[] GetSupportedAttributes()
        {
            return _supportedAttributes;
        }

        protected override void WriteTrace(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message, Guid? relatedActivityId, object[] data)
        {
            string dataString = null;
            if (data != null)
            {
                dataString = JsonConvert.SerializeObject(data);
            }
            WriteToDatabase(eventCache, source, eventType, id, message, relatedActivityId, dataString);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.DateTimeOffset", Justification = "Deliberate dependency, .NET 2.0 SP1 required.")]
        private void WriteToDatabase(TraceEventCache eventCache, string source, TraceEventType eventType, int? id, string message, Guid? relatedActivityId, string dataString)
        {
            DateTime logTime;
            string logicalOperationStack = null;
            if (eventCache != null)
            {
                logTime = eventCache.DateTime.ToUniversalTime();
                if (eventCache.LogicalOperationStack != null && eventCache.LogicalOperationStack.Count > 0)
                {
                    StringBuilder stackBuilder = new StringBuilder();
                    foreach (object o in eventCache.LogicalOperationStack)
                    {
                        if (stackBuilder.Length > 0) stackBuilder.Append(", ");
                        stackBuilder.Append(o);
                    }
                    logicalOperationStack = stackBuilder.ToString();
                }
            }
            else
            {
                logTime = DateTimeOffset.UtcNow.UtcDateTime;
            }

            object threadId = eventCache != null ? (object)eventCache.ThreadId : DBNull.Value;
            object thread = Thread.CurrentThread.Name ?? threadId;

            // Truncate message
            int maxLength = MaxMessageLength;
            const string trimmedMessageIndicator = "...";
            if (message != null && message.Length > maxLength)
            {
                message = message.Substring(0, maxLength - trimmedMessageIndicator.Length) + trimmedMessageIndicator;
            }

            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings[ConnectionName];
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connectionSettings.ProviderName);
            using (var connection = DbProviderFactoryExtensions.CreateConnection(dbFactory, connectionSettings.ConnectionString))
            {
                using (var command = DbProviderFactoryExtensions.CreateCommand(dbFactory, CommandText, connection))
                {
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@ApplicationName", ApplicationName != null ? (object)ApplicationName : DBNull.Value));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@Source", source != null ? (object)source : DBNull.Value));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@Id", id ?? 0));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@EventType", eventType.ToString()));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@UtcDateTime", logTime));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@DateTime", logTime));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@MachineName", Environment.MachineName));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@AppDomainFriendlyName", AppDomain.CurrentDomain.FriendlyName));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@ProcessId", eventCache != null ? (object)eventCache.ProcessId : 0));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@ThreadName", thread));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@ThreadId", threadId));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@Message", message != null ? (object)message : DBNull.Value));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@ActivityId", Trace.CorrelationManager.ActivityId != Guid.Empty ? (object)Trace.CorrelationManager.ActivityId : DBNull.Value));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@RelatedActivityId", relatedActivityId.HasValue ? (object)relatedActivityId.Value : DBNull.Value));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@LogicalOperationStack", logicalOperationStack != null ? (object)logicalOperationStack : DBNull.Value));
                    command.Parameters.Add(DbProviderFactoryExtensions.CreateParameter(dbFactory, "@Data", dataString != null ? (object)dataString : DBNull.Value));

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
