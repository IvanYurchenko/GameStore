using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ControllerActionRoute",
                url: "{controller}/{action}",
                defaults: new { controller = "Game", action = "Index" });

            routes.MapRoute(
                name: "GameRoute",
                url: "{controller}/{key}/{action}",
                defaults: new { controller = "Game", key = UrlParameter.Optional, action = "Details" }
                );
        }
    }
}