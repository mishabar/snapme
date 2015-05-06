using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SNAPME.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "SaleDetails",
            //    url: "sale/{id}",
            //    defaults: new { controller = "Sale", action = "Details" },
            //    namespaces: new string[] { "SNAPME.Web.Controllers" }
            //);

            routes.MapRoute(
                name: "AccountRoutes",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "Details", id = UrlParameter.Optional },
                namespaces: new string[] { "SNAPME.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SNAPME.Web.Controllers" }
            );
        }
    }
}
