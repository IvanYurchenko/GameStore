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
                name: "ValidationRoute",
                url: "Validation/{action}",
                defaults: new { controller = "Validation" });

            routes.MapRoute(
                name: "PublishersRoute",
                url: "Publisher/Publishers",
                defaults: new { controller = "Publisher", action = "Publishers" });

            routes.MapRoute(
                name: "NewRoute",
                url: "{controller}/new",
                defaults: new { action = "New" });

            routes.MapRoute(
                name: "PublisherCompanyNameRoute",
                url: "Publisher/{key}",
                defaults: new { controller = "Publisher", action = "Details" });

            routes.MapRoute(
                name: "BuyRoute",
                url: "Game/{key}/Buy",
                defaults: new { controller = "Basket", action = "Add" });

            routes.MapRoute(
                name: "CommentsRoute",
                url: "Game/{key}/{action}",
                defaults: new { controller = "Comment" },
                constraints: new { action = "^comments|^newcomment" });

            routes.MapRoute(
                name: "BasketRoute",
                url: "Basket/{action}",
                defaults: new { controller = "Basket", action = "Index" });

            routes.MapRoute(
                name: "GameRoute",
                url: "{controller}/{key}/{action}",
                defaults: new { controller = "Game", action = "Details" });

            routes.MapRoute(
                name: "DefaultRoute",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Games", id = UrlParameter.Optional });
        }
    }
}