using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Games", "games/{action}", new { controller = "GameJson", action = "Games" }
                );

            routes.MapRoute("GameDetails", "game/{key}", new { controller = "GameJson", action = "Details" }
                );

            routes.MapRoute("Game", "game/{key}/{action}", new { controller = "GameJson" }
                );

            routes.MapRoute("Default", "{controller}/{action}/{key}",
                new { controller = "GameJson", action = "Games", key = UrlParameter.Optional }
                );

            //routes.MapRoute(
            //    name: "ValidationRoute",
            //    url: "Validation/{action}",
            //    defaults: new { controller = "Validation" });

            //routes.MapRoute(
            //    name: "NewRoute",
            //    url: "{controller}/new",
            //    defaults: new { action = "New" });

            //routes.MapRoute(
            //    name: "BuyRoute",
            //    url: "Game/{key}/Buy",
            //    defaults: new { controller = "Basket", action = "Add" });

            //routes.MapRoute(
            //    name: "CommentsRoute",
            //    url: "Game/{key}/{action}",
            //    defaults: new { controller = "Comment" },
            //    constraints: new { action = "^comments|^newcomment" });

            //routes.MapRoute(
            //    name: "GameRoute",
            //    url: "{controller}/{key}/{action}",
            //    defaults: new { controller = "Game", action = "Details" });

            //routes.MapRoute(
            //    name: "DefaultRoute",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Game", action = "Games", id = UrlParameter.Optional });
        }
    }
}