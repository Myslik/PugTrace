using Microsoft.Owin;
using Owin;
using PugTrace.SqlServer;

[assembly: OwinStartup(typeof(PugTrace.Example.Startup))]

namespace PugTrace.Example
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UsePugTraceDashboard();
        }
    }
}
