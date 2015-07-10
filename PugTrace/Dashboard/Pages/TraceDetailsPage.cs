namespace PugTrace.Dashboard.Pages
{
    partial class TraceDetailsPage
    {
        public TraceDetailsPage(string traceId)
        {
            TraceId = traceId;
        }

        public string TraceId { get; private set; }
    }
}