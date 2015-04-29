using System.Web.Routing;
using GameStore.WebUI.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.WebUI.Tests
{
    [TestClass]
    public class RouteConfigTest
    {
        #region ValidationRoute

        [TestMethod]
        public void Check_Route_For_GameKey_Validation()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/validation/isgamekeyavailable");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/validation/iscompanynameavailable");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Validation", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("IsCompanyNameAvailable", routeData.Values["action"].ToString(), true);
        }

        #endregion

        #region GetRoute

        [TestMethod]
        public void Check_Route_For_Getting_Publishers_Explicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/publisher/get");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/publisher");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/game/get");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/game");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/basket/get");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/basket");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Basket", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("Get", routeData.Values["action"].ToString(), true);
        }

        #endregion

        #region NewRoute

        [TestMethod]
        public void Check_Route_For_New_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/new");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/publisher/new");
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Publisher", routeData.Values["controller"].ToString(), true);
            Assert.AreEqual("New", routeData.Values["action"].ToString(), true);
        }

        #endregion

        #region BuyRoute

        [TestMethod]
        public void Check_Route_For_Buy_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey/buy");
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

        #endregion

        #region CommentsRoute
        
        [TestMethod]
        public void Check_Route_For_Getting_Comments_For_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey/comments");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey/newcomment");
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

        #endregion

        #region BasicRoute

        #region Details 

        [TestMethod]
        public void Check_Route_For_Getting_Game_Details_Explicitly()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey/details");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/publisher/testcompanyname/details");
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
            var context = new StubHttpContextForRouting(requestUrl: "~/publisher/testcompanyname");
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

        #endregion

        #region Update

        [TestMethod]
        public void Check_Route_For_Updating_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey/update");
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

        #endregion

        #region Remove
        
        [TestMethod]
        public void Check_Route_For_Removing_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey/remove");
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

        #endregion

        #region Download

        [TestMethod]
        public void Check_Route_For_Downloading_Game()
        {
            // Arrange
            var context = new StubHttpContextForRouting(requestUrl: "~/game/testkey/download");
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

        #endregion

        #endregion
    }
}