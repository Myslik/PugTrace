using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PugTrace.Dashboard
{
    public class RequestDispatcherContext
    {
        public RequestDispatcherContext(
            string appPath,
            TraceStorage storage,
            IDictionary<string, object> owinEnvironment,
            Match uriMatch)
        {
            if (appPath == null) throw new ArgumentNullException("appPath");
            if (storage == null) throw new ArgumentNullException("storage");
            if (owinEnvironment == null) throw new ArgumentNullException("owinEnvironment");
            if (uriMatch == null) throw new ArgumentNullException("uriMatch");

            AppPath = appPath;
            Storage = storage;
            OwinEnvironment = owinEnvironment;
            UriMatch = uriMatch;
        }

        public string AppPath { get; private set; }
        public TraceStorage Storage { get; private set; }
        public IDictionary<string, object> OwinEnvironment { get; private set; }
        public Match UriMatch { get; private set; }
    }
}
