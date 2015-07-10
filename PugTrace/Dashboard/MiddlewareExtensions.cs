using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PugTrace.Dashboard
{
    using BuildFunc = Action<
        Func<
            IDictionary<string, object>,
            Func<
                Func<IDictionary<string, object>, Task>,
                Func<IDictionary<string, object>, Task>
        >>>;
    using MidFunc = Func<
            Func<IDictionary<string, object>, Task>,
            Func<IDictionary<string, object>, Task>
            >;
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class MiddlewareExtensions
    {
        public static BuildFunc UsePugTraceDashboard(
            this BuildFunc builder,
            DashboardOptions options,
            TraceStorage storage,
            RouteCollection routes)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (options == null) throw new ArgumentNullException("options");
            if (routes == null) throw new ArgumentNullException("routes");

            builder(_ => UsePugTraceDashboard(options, storage, routes));

            return builder;
        }

        public static MidFunc UsePugTraceDashboard(
            DashboardOptions options,
            TraceStorage storage,
            RouteCollection routes)
        {
            if (options == null) throw new ArgumentNullException("options");
            if (routes == null) throw new ArgumentNullException("routes");

            return
                next =>
                env =>
                {
                    var context = new OwinContext(env);
                    var dispatcher = routes.FindDispatcher(context.Request.Path.Value);

                    if (dispatcher == null)
                    {
                        return next(env);
                    }

                    if (options.AuthorizationFilters.Any(filter => !filter.Authorize(context.Environment)))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.FromResult(false);
                    }

                    var dispatcherContext = new RequestDispatcherContext(
                        options.AppPath,
                        storage,
                        context.Environment,
                        dispatcher.Item2);

                    return dispatcher.Item1.Dispatch(dispatcherContext);
                };
        }
    }
}