using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("ValidationRoute", "Validation/{action}", new {controller = "Validation"});

            routes.MapRoute("PublishersRoute", "Publisher/Publishers",
                new {controller = "Publisher", action = "Publishers"});

            routes.MapRoute("NewRoute", "{controller}/new", new {action = "New"});

            //routes.MapRoute(
            //    name: "IndexRoute",
            //    url: "{controller}/Index",
            //    defaults: new { action = "Index" });

            routes.MapRoute("PublisherCompanyNameRoute", "Publisher/{key}",
                new {controller = "Publisher", action = "Details"});

            routes.MapRoute("BuyRoute", "Game/{key}/Buy", new {controller = "Basket", action = "Add"});

            routes.MapRoute("CommentsRoute", "Game/{key}/{action}", new {controller = "Comment"},
                new {action = "^comments|^newcomment"});

            routes.MapRoute("BasketRoute", "Basket/{action}", new {controller = "Basket", action = "Index"});

            routes.MapRoute("GameRoute", "{controller}/{key}/{action}", new {controller = "Game", action = "Details"});

            routes.MapRoute("DefaultRoute", "{controller}/{action}/{id}",
                new {controller = "Game", action = "Games", id = UrlParameter.Optional});
        }
    }
}