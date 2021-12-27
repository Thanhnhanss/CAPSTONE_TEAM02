using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VanLangDoctor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "search",
                url: "tim-kiem",
                defaults: new { controller = "BACSIsAdmin", action = "search", id = UrlParameter.Optional }
            );
        }
    }
}
