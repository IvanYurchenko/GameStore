using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("ValidationRoute", "Validation/{action}", new { controller = "Validation" });

            routes.MapRoute("GetRoute", "{controller}/get", new { action = "Get" });

            routes.MapRoute("NewRoute", "{controller}/new", new { action = "New" });

            routes.MapRoute("PublisherCompanyNameRoute", "Publisher/{key}",
                new { controller = "Publisher", action = "Details" });

            routes.MapRoute("BuyRoute", "Game/{key}/Buy", new { controller = "Basket", action = "Add" });

            routes.MapRoute("CommentsRoute", "Game/{key}/{action}", new { controller = "Comment" },
                new { action = "^comments|^newcomment" });

            routes.MapRoute("BasketRoute", "Basket/{action}", new { controller = "Basket", action = "Get" });

            routes.MapRoute("GameRoute", "Game/{key}/{action}", new { controller = "Game", action = "Details" });

            routes.MapRoute("DefaultRoute", "{controller}/{action}/{id}",
                new { controller = "Game", action = "Get", id = UrlParameter.Optional });
        }
    }
}