using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Games", "games/{action}", new {controller = "GamesJson", action = "Games"}
                );

            routes.MapRoute("GameDetails", "game/{key}", new {controller = "GamesJson", action = "Details"}
                );

            routes.MapRoute("Game", "game/{key}/{action}", new {controller = "GamesJson"}
                );

            routes.MapRoute("Default", "{controller}/{action}/{key}",
                new {controller = "Games", action = "Games", key = UrlParameter.Optional}
                );
        }
    }
}