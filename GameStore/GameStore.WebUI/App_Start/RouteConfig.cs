using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        private const string DefaultLanguage = "en";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("ValidationRoute", "{lang}/Validation/{action}", new { controller = "Validation", lang = DefaultLanguage });

            routes.MapRoute("ErrorRoute", "{lang}/Error/{action}", new { controller = "Error", lang = DefaultLanguage });

            routes.MapRoute("AccountRoute", "{lang}/Account/{action}", new { controller = "Account", lang = DefaultLanguage });

            routes.MapRoute("UserRoute", "{lang}/User/{action}", new { controller = "User", lang = DefaultLanguage });

            routes.MapRoute("GenreRoute", "{lang}/Genre/{action}", new { controller = "Genre", lang = DefaultLanguage });

            routes.MapRoute("RoleRoute", "{lang}/Role/{action}", new { controller = "Role", lang = DefaultLanguage });

            routes.MapRoute("OrdersRoute", "{lang}/Orders/", new { controller = "Order", action = "Orders", lang = DefaultLanguage });

            routes.MapRoute("OrderRoute", "{lang}/Order/{action}", new { controller = "Order", lang = DefaultLanguage });

            routes.MapRoute("GetRoute", "{lang}/{controller}/Get", new { action = "Get", lang = DefaultLanguage });

            routes.MapRoute("NewRoute", "{lang}/{controller}/New", new { action = "New", lang = DefaultLanguage });

            routes.MapRoute("BuyRoute", "{lang}/Game/{key}/Buy", new { controller = "Basket", action = "Add", lang = DefaultLanguage });

            routes.MapRoute("CommentsRoute", "{lang}/Game/{key}/{action}", new { controller = "Comment", lang = DefaultLanguage },
                new {action = "^Comments|^NewComment"});

            routes.MapRoute("BasicRoute", "{lang}/{controller}/{key}/{action}", new { action = "Details", lang = DefaultLanguage });

            routes.MapRoute("DefaultRoute", "{lang}/{controller}/{action}",
                new { controller = "Game", action = "Get", lang = DefaultLanguage });
        }
    }
}