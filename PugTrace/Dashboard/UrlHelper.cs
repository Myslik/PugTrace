using Microsoft.Owin;
using System;
using System.Collections.Generic;

namespace PugTrace.Dashboard
{
    public class UrlHelper
    {
        private readonly OwinContext _context;

        public UrlHelper(IDictionary<string, object> owinContext)
        {
            if (owinContext == null) throw new ArgumentNullException("owinContext");
            _context = new OwinContext(owinContext);
        }

        public string To(string relativePath)
        {
            return _context.Request.PathBase + relativePath;
        }

        public string Home()
        {
            return To("/");
        }

        public string TraceDetails(string traceId)
        {
            return To("/traces/" + traceId);
        }
    }
}
