using PugTrace.Dashboard.Pages;
using System;

namespace PugTrace.Dashboard
{
    public static class DashboardRoutes
    {
        private static readonly string[] Javascripts =
        {
            "jquery-1.11.3.min.js", 
            "bootstrap.min.js",
            "highlight.pack.js"
        };

        private static readonly string[] Stylesheets =
        {
            "bootstrap.min.css",
            "vs.css",
            "pugtrace.css"
        };

        static DashboardRoutes()
        {
            Routes = new RouteCollection();
            Routes.AddRazorPage("/", x => new HomePage());
            Routes.AddRazorPage("/traces/(?<TraceId>.+)", x => new TraceDetailsPage(x.Groups["TraceId"].Value));

            #region Embedded static content

            Routes.Add("/img/logo", new EmbeddedResourceDispatcher(
                "image/png",
                typeof(DashboardRoutes).Assembly,
                GetContentResourceName("img", "pugtrace.png")));

            Routes.Add("/js", new CombinedResourceDispatcher(
                "application/javascript",
                typeof(DashboardRoutes).Assembly,
                GetContentFolderNamespace("js"),
                Javascripts));

            Routes.Add("/css", new CombinedResourceDispatcher(
                "text/css",
                typeof(DashboardRoutes).Assembly,
                GetContentFolderNamespace("css"),
                Stylesheets));

            Routes.Add("/fonts/glyphicons-halflings-regular/eot", new EmbeddedResourceDispatcher(
                "application/vnd.ms-fontobject",
                typeof(DashboardRoutes).Assembly,
                GetContentResourceName("fonts", "glyphicons-halflings-regular.eot")));

            Routes.Add("/fonts/glyphicons-halflings-regular/svg", new EmbeddedResourceDispatcher(
                "image/svg+xml",
                typeof(DashboardRoutes).Assembly,
                GetContentResourceName("fonts", "glyphicons-halflings-regular.svg")));

            Routes.Add("/fonts/glyphicons-halflings-regular/ttf", new EmbeddedResourceDispatcher(
                "application/octet-stream",
                typeof(DashboardRoutes).Assembly,
                GetContentResourceName("fonts", "glyphicons-halflings-regular.ttf")));

            Routes.Add("/fonts/glyphicons-halflings-regular/woff", new EmbeddedResourceDispatcher(
                "application/font-woff",
                typeof(DashboardRoutes).Assembly,
                GetContentResourceName("fonts", "glyphicons-halflings-regular.woff")));

            Routes.Add("/fonts/glyphicons-halflings-regular/woff2", new EmbeddedResourceDispatcher(
                "application/font-woff2",
                typeof(DashboardRoutes).Assembly,
                GetContentResourceName("fonts", "glyphicons-halflings-regular.woff2")));

            #endregion
        }

        public static RouteCollection Routes { get; private set; }

        internal static string GetContentFolderNamespace(string contentFolder)
        {
            return String.Format("{0}.Content.{1}", typeof(DashboardRoutes).Namespace, contentFolder);
        }

        internal static string GetContentResourceName(string contentFolder, string resourceName)
        {
            return String.Format("{0}.{1}", GetContentFolderNamespace(contentFolder), resourceName);
        }
    }
}