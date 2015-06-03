using System.Web.Routing;
using GameStore.WebUI.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.WebUI.Tests
{
    [TestClass]
    public class RouteConfigTest
    {
        [TestMethod]
        public void Check_Route_For_GameKey_Validation()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/validation/isgamekeyavailable");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Validation", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("IsGameKeyAvailable", routeData.Values["action"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_CompanyName_Validation()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/validation/iscompanynameavailable");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Validation", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("IsCompanyNameAvailable", routeData.Values["action"].ToString(), true);
        }
        
        [TestMethod]
        public void Check_Route_For_Getting_Publishers_Explicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/publisher/get");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Publisher", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Get", routeData.Values["action"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Getting_Publishers_Implicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/publisher");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Publisher", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Get", routeData.Values["action"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Getting_Games_Explicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/get");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Get", routeData.Values["action"].ToString(), true);
        }


        [TestMethod]
        public void Check_Route_For_Getting_Games_Implicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Get", routeData.Values["action"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Getting_Basket_Explicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/basket/get");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Basket", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Get", routeData.Values["action"].ToString(), true);
        }


        [TestMethod]
        public void Check_Route_For_Getting_Basket_Implicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/basket");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Basket", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Get", routeData.Values["action"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_New_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/new");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("New", routeData.Values["action"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_New_Publisher()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/publisher/new");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Publisher", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("New", routeData.Values["action"].ToString(), true);
        }
        
        [TestMethod]
        public void Check_Route_For_Buy_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey/buy");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Basket", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Add", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }
        
        [TestMethod]
        public void Check_Route_For_Getting_Comments_For_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey/comments");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Comment", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Comments", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Adding_Comment_For_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey/newcomment");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Comment", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("NewComment", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Getting_Game_Details_Explicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey/details");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Details", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Getting_Game_Details_Implicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Details", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Getting_Publisher_Details_Explicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/publisher/testcompanyname/details");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Publisher", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Details", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testcompanyname", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Getting_Publisher_Details_Implicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/publisher/testcompanyname");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Publisher", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Details", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testcompanyname", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Updating_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey/update");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Update", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Removing_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey/remove");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Remove", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }

        [TestMethod]
        public void Check_Route_For_Downloading_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/en/game/testkey/download");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Download", routeData.Values["action"].ToString(), true);
            Assert.AreEqual("testkey", routeData.Values["key"].ToString(), true);
        }
    }
}