using System;

namespace PugTrace.Storage
{
    public class Trace
    {
        public string ApplicationName { get; set; }
        public int TraceId { get; set; }
        public string Source { get; set; }
        public int Id { get; set; }
        public string EventType { get; set; }
        public DateTime UtcDateTime { get; set; }
        public string MachineName { get; set; }
        public string AppDomainFriendlyName { get; set; }
        public int ProcessId { get; set; }
        public string ThreadName { get; set; }
        public string Message { get; set; }
        public Guid? ActivityId { get; set; }
        public Guid? RelatedActivityId { get; set; }
        public string LogicalOperationStack { get; set; }
        public string Data { get; set; }
        public string PrincipalIdentityName { get; set; }
    }
}