using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nyika.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Shop", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "MF", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { area = "Stock", controller = "DashboardSh", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
            //    new[] { "Nyika.WebUI.Areas.Stock.Controllers" }
            //).DataTokens.Add("area", "Stock");


        }
    }
}
