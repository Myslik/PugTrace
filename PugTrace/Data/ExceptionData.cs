using System;
using System.Security;
using System.Threading;
using System.Web;

namespace PugTrace.Data
{
    public class ExceptionData : ITraceData
    {
        public string HostName { get; private set; }
        public string TypeName { get; private set; }
        public string Message { get; private set; }
        public string Source { get; private set; }
        public string Detail { get; private set; }
        public string User { get; private set; }
        public DateTime Time { get; private set; }
        public int StatusCode { get; private set; }

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

            var httpException = e as HttpException;

            if (httpException != null)
            {
                StatusCode = httpException.GetHttpCode();
            }
        }
    }
}