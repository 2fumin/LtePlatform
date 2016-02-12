using Microsoft.Owin.Host.SystemWeb;
using Owin;

namespace System.Web.Routing
{
    public static class RouteCollectionExtensions
    {
        private static T Add<T>(RouteCollection routes, string name, T item) where T : RouteBase
        {
            if (string.IsNullOrEmpty(name))
            {
                routes.Add(item);
                return item;
            }
            routes.Add(name, item);
            return item;
        }

        public static RouteBase MapOwinPath(this RouteCollection routes, string pathBase)
        {
            return Add(routes, null, new OwinRoute(pathBase, OwinApplication.Accessor));
        }

        public static RouteBase MapOwinPath(this RouteCollection routes, string pathBase, Action<IAppBuilder> startup)
        {
            var appDelegate = OwinBuilder.Build(startup);
            return Add(routes, null, new OwinRoute(pathBase, () => appDelegate));
        }

        public static RouteBase MapOwinPath(this RouteCollection routes, string name, string pathBase)
        {
            return Add(routes, name, new OwinRoute(pathBase, OwinApplication.Accessor));
        }

        public static RouteBase MapOwinPath<TApp>(this RouteCollection routes, string pathBase, TApp app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var appDelegate = OwinBuilder.Build(delegate (IAppBuilder builder) {
                builder.Use(app);
            });
            return Add(routes, null, new OwinRoute(pathBase, () => appDelegate));
        }

        public static RouteBase MapOwinPath(this RouteCollection routes, string name, string pathBase, Action<IAppBuilder> startup)
        {
            var appDelegate = OwinBuilder.Build(startup);
            return Add(routes, name, new OwinRoute(pathBase, () => appDelegate));
        }

        public static RouteBase MapOwinPath<TApp>(this RouteCollection routes, string name, string pathBase, TApp app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var appDelegate = OwinBuilder.Build(delegate (IAppBuilder builder) {
                builder.Use(app);
            });
            return Add(routes, name, new OwinRoute(pathBase, () => appDelegate));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeUrl, Action<IAppBuilder> startup)
        {
            return Add(routes, null, new Route(routeUrl, new OwinRouteHandler(startup)));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeName, string routeUrl, Action<IAppBuilder> startup)
        {
            return Add(routes, routeName, new Route(routeUrl, new OwinRouteHandler(startup)));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeUrl, RouteValueDictionary defaults, Action<IAppBuilder> startup)
        {
            return Add(routes, null, new Route(routeUrl, defaults, new OwinRouteHandler(startup)));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeName, string routeUrl, RouteValueDictionary defaults, Action<IAppBuilder> startup)
        {
            return Add(routes, routeName, new Route(routeUrl, defaults, new OwinRouteHandler(startup)));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeUrl, RouteValueDictionary defaults, RouteValueDictionary constraints, Action<IAppBuilder> startup)
        {
            return Add(routes, null, new Route(routeUrl, defaults, constraints, new OwinRouteHandler(startup)));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeName, string routeUrl, RouteValueDictionary defaults, RouteValueDictionary constraints, Action<IAppBuilder> startup)
        {
            return Add(routes, routeName, new Route(routeUrl, defaults, constraints, new OwinRouteHandler(startup)));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeUrl, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, Action<IAppBuilder> startup)
        {
            return Add(routes, null, new Route(routeUrl, defaults, constraints, dataTokens, new OwinRouteHandler(startup)));
        }

        public static Route MapOwinRoute(this RouteCollection routes, string routeName, string routeUrl, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, Action<IAppBuilder> startup)
        {
            return Add(routes, routeName, new Route(routeUrl, defaults, constraints, dataTokens, new OwinRouteHandler(startup)));
        }
    }
}
