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

            routes.MapRoute("OrdersHistory", "Order/History", new { controller = "Order", action = "History" });

            routes.MapRoute("GetRoute", "{controller}/Get", new {action = "Get"});

            routes.MapRoute("NewRoute", "{controller}/New", new {action = "New"});

            routes.MapRoute("BuyRoute", "Game/{key}/Buy", new {controller = "Basket", action = "Add"});

            routes.MapRoute("CommentsRoute", "Game/{key}/{action}", new {controller = "Comment"},
                new {action = "^Comments|^NewComment"});

            routes.MapRoute("BasicRoute", "{controller}/{key}/{action}", new {action = "Details"});

            routes.MapRoute("DefaultRoute", "{controller}/{action}/{id}",
                new {controller = "Game", action = "Get", id = UrlParameter.Optional});
        }
    }
}