using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            // name: "Games",
            // url: "games/{action}",
            // defaults: new { controller = "GamesJson", action = "Games" }
            //);

            //routes.MapRoute(
            //    name: "GameDetails",
            //    url: "game/{key}",
            //    defaults: new { controller = "GamesJson", action = "Details" }
            //);

            //routes.MapRoute(
            //    name: "Game",
            //    url: "game/{key}/{action}",
            //    defaults: new { controller = "GamesJson" }
            //);

            routes.MapRoute("Default", "{controller}/{action}/{key}",
                new {controller = "Games", action = "Games", key = UrlParameter.Optional}
                );
        }
    }
}