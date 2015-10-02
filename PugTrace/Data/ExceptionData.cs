using System;
using System.Security;
using System.Threading;

namespace PugTrace.Data
{
    public class ExceptionData : ITraceData
    {
        public string HostName { get; set; }
        public string TypeName { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string Detail { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }

        public bool IsValid ()
        {
            return !string.IsNullOrEmpty(TypeName)
                && !string.IsNullOrEmpty(Message)
                && !string.IsNullOrEmpty(Source);
        }

        public ExceptionData()
        {

        }

        public ExceptionData(Exception e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            try
            {
                HostName = Environment.MachineName;
            }
            catch (SecurityException)
            {
                HostName = string.Empty;
            }

            TypeName = e.GetType().FullName;
            Message = e.Message;
            Source = e.Source;
            Detail = e.ToString();
            User = Thread.CurrentPrincipal.Identity.Name ?? string.Empty;
            Time = DateTime.Now;
        }
    }
}