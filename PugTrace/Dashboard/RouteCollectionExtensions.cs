using System;
using System.Text.RegularExpressions;

namespace PugTrace.Dashboard
{
    public static class RouteCollectionExtensions
    {
        public static void AddRazorPage(
            this RouteCollection routes,
            string pathTemplate,
            Func<Match, RazorPage> pageFunc)
        {
            if (routes == null) throw new ArgumentNullException("routes");
            if (pathTemplate == null) throw new ArgumentNullException("pathTemplate");
            if (pageFunc == null) throw new ArgumentNullException("pageFunc");

            routes.Add(pathTemplate, new RazorPageDispatcher(pageFunc));
        }
    }
}