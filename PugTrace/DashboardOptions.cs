using PugTrace.Dashboard;
using System.Collections.Generic;

namespace PugTrace
{
    public class DashboardOptions
    {
        public DashboardOptions()
        {
            AppPath = "/";
            AuthorizationFilters = new[] { new LocalRequestsOnlyAuthorizationFilter() };
        }

        public string AppPath { get; set; }
        public IEnumerable<IAuthorizationFilter> AuthorizationFilters { get; set; }
    }
}