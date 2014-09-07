using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tmc.Core.Infrastructure;
using Tmc.Web.Framework.Routes;
using Tmc.Core.DependencyManagement;

namespace Tmc.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            var routePublisher = EngineContext.Current.Resolve<IRouteRegistrar>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional},
                namespaces: new[] { "Tmc.Web.Controllers" }
            );

            routes.MapRoute("HomePage",
                "Home",
                new { controller = "Home", action = "Index" },
                new[] { "Tmc.Web.Controllers" });

            routes.MapRoute("AdminHomePage",
            "Admin",
            new { controller = "Home", action = "Index" },
            new[] { "Tmc.Admin.Controllers" });

            routes.MapRoute("ManageTransactions",
            "Transaction/List",
            new { controller = "Transaction", action = "List" },
            new[] { "Tmc.Admin.Controllers" });

            //routes.MapRoute("Logout",
            //                "logout",
            //                new { controller = "Customer", action = "Logout" },
            //                new[] { "Tmc.Web.Controllers" });
        }
    }
}
