using Microsoft.Owin.Infrastructure;
using Owin;
using PugTrace.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PugTrace
{
    using BuildFunc = Action<
                    Func<
                        IDictionary<string, object>,
                        Func<
                            Func<IDictionary<string, object>, Task>,
                            Func<IDictionary<string, object>, Task>
                    >>>;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UsePugTraceDashboard(this IAppBuilder builder)
        {
            return builder.UsePugTraceDashboard("/pugtrace");
        }

        public static IAppBuilder UsePugTraceDashboard(
            this IAppBuilder builder,
            string pathMatch)
        {
            return builder.UsePugTraceDashboard(pathMatch, new DashboardOptions());
        }

        public static IAppBuilder UsePugTraceDashboard(
            this IAppBuilder builder,
            string pathMatch,
            DashboardOptions options)
        {
            return builder.UsePugTraceDashboard(pathMatch, options, TraceStorage.Current);
        }

        public static IAppBuilder UsePugTraceDashboard(
            this IAppBuilder builder,
            string pathMatch,
            DashboardOptions options,
            TraceStorage storage)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (pathMatch == null) throw new ArgumentNullException("pathMatch");
            if (options == null) throw new ArgumentNullException("options");
            if (storage == null) throw new ArgumentNullException("storage");

            SignatureConversions.AddConversions(builder);

            builder.Map(pathMatch, subApp => subApp
                .UseOwin()
                .UsePugTraceDashboard(options, storage, DashboardRoutes.Routes));

            return builder;
        }

        private static BuildFunc UseOwin(this IAppBuilder builder)
        {
            return middleware => builder.Use(middleware(builder.Properties));
        }
    }
}