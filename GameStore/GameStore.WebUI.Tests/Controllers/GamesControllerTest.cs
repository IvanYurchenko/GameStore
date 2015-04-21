using System.Collections.Generic;
using System.Web.Mvc;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.WebUI.Tests.Controllers
{
    //[TestClass]
    //public class GamesControllerTest
    //{
    //    [TestInitialize]
    //    public void Initialize()
    //    {
    //        Mapping.MapInit();
    //    }
        
    //    [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_Games_Action()
    //    {
    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        mockGameService.Setup(m => m.GetAll()).Verifiable();
    //        var mockCommentService = new Mock<ICommentService>();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();
            
    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        // Act
    //        gamesController.GetGames();

    //        // Assert
    //        mockGameService.Verify(m => m.GetAll());
    //    }

    //            [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_Details_Action()
    //    {
    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        mockGameService.Setup(m => m.GetGameModelById(It.IsAny<int>())).Verifiable();
    //        var mockCommentService = new Mock<ICommentService>();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();

    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        const int testId = 1;

    //        // Act
    //        gamesController.GetGameDetails(testId);

    //        // Assert
    //        mockGameService.Verify(m => m.GetGameModelById(testId));
    //    }


    //    [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_New_Action()
    //    {
    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        mockGameService.Setup(m => m.Add(It.IsAny<GameModel>())).Verifiable();
    //        var mockCommentService = new Mock<ICommentService>();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();

    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        var testModel = new GameModel();

    //        // Act
    //        gamesController.AddGame(testModel);

    //        // Assert
    //        mockGameService.Verify(m => m.Add(testModel));
    //    }

    //    [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_Update_Action()
    //    {
    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        mockGameService.Setup(m => m.Update(It.IsAny<GameModel>())).Verifiable();
    //        var mockCommentService = new Mock<ICommentService>();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();

    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        var testModel = new GameModel();

    //        // Act
    //        gamesController.UpdateGame(testModel);

    //        // Assert
    //        mockGameService.Verify(m => m.Update(testModel));
    //    }

    //    [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_Remove_Action()
    //    {
    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        mockGameService.Setup(m => m.Remove(It.IsAny<GameModel>())).Verifiable();
    //        var mockCommentService = new Mock<ICommentService>();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();

    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        var testKey = "testKey";

    //        // Act
    //        gamesController.RemoveGame(testKey);

    //        // Assert
    //        mockGameService.Verify(m => m.Remove(testKey));
    //    }

    //    [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_Download_Action()
    //    {
    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        var mockCommentService = new Mock<ICommentService>();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();

    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        var testKey = "testKey";

    //        // Act
    //        var actionResult = gamesController.DownloadGame(testKey);

    //        // Assert
    //        Assert.IsTrue(actionResult is FileResult);
    //    }

    //    [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_NewComment_Action()
    //    {
    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        var mockCommentService = new Mock<ICommentService>();
    //        mockCommentService.Setup(m => m.Add(It.IsAny<CommentModel>(), It.IsAny<string>())).Verifiable();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();

    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        var testModel = new CommentModel();
    //        var testKey = "testKey";

    //        // Act
    //        //gamesController.AddComment(testKey, testModel);

    //        // Assert
    //        mockCommentService.Verify(m => m.Add(testModel, testKey));
    //    }

    //    [TestMethod]
    //    public void Check_That_Right_Method_Was_Called_Inside_Comments_Action()
    //    {
    //        var gameModel = new GameModel
    //        {
    //            GameId = 2,
    //            Key = "testKey",
    //            Comments = new List<CommentModel>
    //            {
    //                new CommentModel {CommentId = 1, Body = "Test Body", GameId = 2, Name = "Test Name"}
    //            }
    //        };

    //        // Arrange
    //        var mockGameService = new Mock<IGameService>();
    //        mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
    //            .Returns<string>(p => gameModel);
    //        var mockCommentService = new Mock<ICommentService>();
    //        var mockGenreService = new Mock<IGenreService>();
    //        var mockPlatformTypeService = new Mock<IPlatformTypeService>();
    //        var mockLogger = new Mock<ILogger>();
    //        var mockBasketService = new Mock<IBasketService>();
    //        var mockBasketItemService = new Mock<IBasketItemService>();
    //        var mockPublisherService = new Mock<IPublisherService>();

    //        var gamesController = new GameController(
    //            mockGameService.Object,
    //            mockCommentService.Object,
    //            mockGenreService.Object,
    //            mockPlatformTypeService.Object,
    //            mockBasketItemService.Object,
    //            mockBasketService.Object,
    //            mockPublisherService.Object,
    //            mockLogger.Object);

    //        var testKey = "testKey";

    //        // Act
    //        //gamesController.GetComments(testKey);

    //        // Assert
    //        mockGameService.Verify(m => m.GetGameModelByKey(testKey));
    //    }
    //}
}