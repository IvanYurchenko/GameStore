using System.Web.Routing;
using GameStore.WebUI.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.WebUI.Tests
{
    [TestClass]
    public class RoutesTest
    {
        [TestMethod]
        public void Check_Route_For_Getting_Games()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/games");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("gamesjson", routeData.Values["controller"].ToString().ToLower());
            Assert.AreEqual("games", routeData.Values["action"].ToString().ToLower());
        }

        [TestMethod]
        public void Check_Route_For_Game_Object_Editing()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/games/update");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("gamesjson", routeData.Values["controller"].ToString().ToLower());
            Assert.AreEqual("update", routeData.Values["action"].ToString().ToLower());
        }

        [TestMethod]
        public void Check_Route_For_Game_Details()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/7");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("gamesjson", routeData.Values["controller"].ToString().ToLower());
            Assert.AreEqual("details", routeData.Values["action"].ToString().ToLower());
            Assert.AreEqual("7", routeData.Values["key"]);
        }

        [TestMethod]
        public void Check_Route_For_Comment_Creating()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/7/newcomment");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("gamesjson", routeData.Values["controller"].ToString().ToLower());
            Assert.AreEqual("newcomment", routeData.Values["action"].ToString().ToLower());
            Assert.AreEqual("7", routeData.Values["key"]);
        }
    }
}
