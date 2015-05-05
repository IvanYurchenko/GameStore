using System;
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
    [TestClass]
    public class GameJsonControllerTest
    {
        private const string TestKey = "testKey";

        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
        }

        #region Helpers

        private static GameJsonController GetGameJsonController(
            IMock<IGameService> mockGameService = null,
            IMock<ICommentService> mockCommentService = null,
            IMock<IGenreService> mockGenreService = null,
            IMock<IPlatformTypeService> mockPlatformTypeService = null,
            IMock<ILogger> mockLogger = null
            )
        {
            mockGameService = mockGameService ?? new Mock<IGameService>();
            mockCommentService = mockCommentService ?? new Mock<ICommentService>();
            mockGenreService = mockGenreService ?? new Mock<IGenreService>();
            mockPlatformTypeService = mockPlatformTypeService ?? new Mock<IPlatformTypeService>();
            mockLogger = mockLogger ?? new Mock<ILogger>();

            var gameJsonController = new GameJsonController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockLogger.Object);

            return gameJsonController;
        }

        #endregion

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Games_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetAll()).Verifiable();

            var gameJsonController = GetGameJsonController(mockGameService);

            // Act
            gameJsonController.GetGames();

            // Assert
            mockGameService.Verify(m => m.GetAll());
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_GetGamesByGenre_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGamesByGenre(It.IsAny<GenreModel>())).Verifiable();

            var gameJsonController = GetGameJsonController(mockGameService);

            var testModel = new GenreModel();

            // Act
            gameJsonController.GetGamesByGenre(testModel);

            // Assert
            mockGameService.Verify(m => m.GetGamesByGenre(testModel));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_GetGamesByPlatformType_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGamesByPlatformType(It.IsAny<PlatformTypeModel>())).Verifiable();

            var gameJsonController = GetGameJsonController(mockGameService);

            var testModel = new PlatformTypeModel();

            // Act
            gameJsonController.GetGamesByPlatformType(testModel);

            // Assert
            mockGameService.Verify(m => m.GetGamesByPlatformType(testModel));
        }


        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Details_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<String>())).Verifiable();

            var gameJsonController = GetGameJsonController(mockGameService);

            // Act
            gameJsonController.GetGameByKey(TestKey);

            // Assert
            mockGameService.Verify(m => m.GetGameModelByKey(TestKey));
        }


        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_New_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Add(It.IsAny<GameModel>())).Verifiable();

            var gameJsonController = GetGameJsonController(mockGameService);

            var testModel = new GameModel();

            // Act
            gameJsonController.AddGame(testModel);

            // Assert
            mockGameService.Verify(m => m.Add(testModel));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Update_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Update(It.IsAny<GameModel>())).Verifiable();

            var gameJsonController = GetGameJsonController(mockGameService);

            var testModel = new GameModel();

            // Act
            gameJsonController.UpdateGame(testModel);

            // Assert
            mockGameService.Verify(m => m.Update(testModel));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Remove_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Remove(It.IsAny<GameModel>())).Verifiable();

            var gameJsonController = GetGameJsonController(mockGameService);

            var testModel = new GameModel();

            // Act
            gameJsonController.RemoveGame(testModel);

            // Assert
            mockGameService.Verify(m => m.Remove(testModel));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Download_Action()
        {
            // Arrange
            var gameJsonController = GetGameJsonController();

            // Act
            var actionResult = gameJsonController.DownloadGame();

            // Assert
            Assert.IsTrue(actionResult is FileResult);
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_NewComment_Action()
        {
            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(m => m.Add(It.IsAny<CommentModel>(), It.IsAny<string>())).Verifiable();

            var gameJsonController = GetGameJsonController(mockCommentService: mockCommentService);

            var testModel = new CommentModel();

            // Act
            gameJsonController.AddComment(TestKey, testModel);

            // Assert
            mockCommentService.Verify(m => m.Add(testModel, TestKey));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Comments_Action()
        {
            var gameModel = new GameModel
            {
                GameId = 2,
                Key = TestKey,
                Comments = new List<CommentModel>
                {
                    new CommentModel {CommentId = 1, Body = "Test Body", GameId = 2, Name = "Test Name"}
                }
            };

            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
                .Returns<string>(p => gameModel);

            var gameJsonController = GetGameJsonController(mockGameService);

            // Act
            gameJsonController.GetComments(TestKey);

            // Assert
            mockGameService.Verify(m => m.GetGameModelByKey(TestKey));
        }
    }
}