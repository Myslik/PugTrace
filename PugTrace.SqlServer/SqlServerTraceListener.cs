using Dapper;
using Newtonsoft.Json;
using PugTrace.Data;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace PugTrace.SqlServer
{
    public class SqlServerTraceListener : TraceListenerBase
    {
        string _connectionName;
        const string _defaultApplicationName = "";
        const int _defaultMaxMessageLength = 1500;

        private static string[] _parameters = new string[]
            {
                "ApplicationName",
                "Source",
                "Id",
                "EventType",
                "UtcDateTime",
                "MachineName",
                "AppDomainFriendlyName",
                "ProcessId",
                "ThreadName",
                "Message",
                "ActivityId",
                "RelatedActivityId",
                "LogicalOperationStack",
                "Data",
                "PrincipalIdentityName"
            };

        private static string _columns = string.Join(", ", _parameters.Select(p => string.Format("[{0}]", p)));
        private static string _values = string.Join(", ", _parameters.Select(p => string.Format("@{0}", p)));
        private static string _commandText = string.Format("insert into [PugTrace].[Trace]({0}) VALUES({1})", _columns, _values);

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
                    return _commandText;
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
                foreach(var item in data)
                {
                    var traceData = item as TraceData;
                    if (traceData != null)
                    {
                        message = traceData.ToString();
                    }
                }

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
            using (var connection = new SqlConnection(connectionSettings.ConnectionString))
            {
                connection.Open();
                connection.Execute(CommandText, new
                {
                    ApplicationName = ApplicationName,
                    Source = source,
                    Id = id ?? 0,
                    EventType = eventType.ToString(),
                    UtcDateTime = logTime,
                    MachineName = Environment.MachineName,
                    AppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName,
                    ProcessId = eventCache != null ? eventCache.ProcessId : 0,
                    ThreadName = thread,
                    ThreadId = threadId,
                    Message = message,
                    ActivityId = Trace.CorrelationManager.ActivityId != Guid.Empty ? (Guid?)Trace.CorrelationManager.ActivityId : null,
                    RelatedActivityId = relatedActivityId,
                    LogicalOperationStack = logicalOperationStack,
                    Data = dataString,
                    PrincipalIdentityName = Thread.CurrentPrincipal.Identity.Name
                });
            }
        }
    }
}
