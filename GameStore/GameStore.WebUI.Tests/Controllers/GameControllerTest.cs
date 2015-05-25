using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Mappings;
using GameStore.WebUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.WebUI.Tests.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        #region Helpers

        private static GameController GetGameController(
            IMock<IGameService> mockGameService = null,
            IMock<ICommentService> mockCommentService = null,
            IMock<IGenreService> mockGenreService = null,
            IMock<IPlatformTypeService> mockPlatformTypeService = null,
            IMock<IBasketService> mockBasketService = null,
            IMock<IPublisherService> mockPublisherService = null,
            IMock<ILogger> mockLogger = null
            )
        {
            mockGameService = mockGameService ?? new Mock<IGameService>();
            mockCommentService = mockCommentService ?? new Mock<ICommentService>();
            mockGenreService = mockGenreService ?? new Mock<IGenreService>();
            mockPlatformTypeService = mockPlatformTypeService ?? new Mock<IPlatformTypeService>();
            mockBasketService = mockBasketService ?? new Mock<IBasketService>();
            mockPublisherService = mockPublisherService ?? new Mock<IPublisherService>();
            mockLogger = mockLogger ?? new Mock<ILogger>();

            var gameController = new GameController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockBasketService.Object,
                mockPublisherService.Object,
                mockLogger.Object);

            return gameController;
        }

        private static GameViewModel GetGameViewModel()
        {
            var gameViewModel = new GameViewModel
            {
                GameId = 1,
                Key = "key",
                Price = (decimal) 5.0,
                Name = "name",
                Discontinued = false,
                Description = "description",
                Publisher = new PublisherModel(),
                SelectedGenresIds = new List<int>(),
                SelectedPlatformTypesIds = new List<int>(),
                SelectedPublisherId = 1,
                UnitsInStock = 50,
                AddedDate = DateTime.UtcNow,
                PublicationDate = DateTime.UtcNow,
                Publishers = new List<PublisherModel>(),
                PlatformTypes = new List<PlatformTypeModel>(),
                Genres = new List<GenreModel>(),
            };

            return gameViewModel;
        }

        #endregion

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Games_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGamesByFilter(It.IsAny<GamesFilterModel>(), It.IsAny<PaginationModel>()))
                .Returns(new GamesTransferModel {Games = new List<GameModel>(), PaginationModel = new PaginationModel()});

            GameController gameController = GetGameController(mockGameService);

            // Act
            gameController.GetGames(new GameIndexViewModel());

            // Assert
            mockGameService.Verify(m => m.GetGamesByFilter(It.IsAny<GamesFilterModel>(), It.IsAny<PaginationModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Details_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
                .Returns(new GameModel
                {
                    Genres = new List<GenreModel>(),
                    PlatformTypes = new List<PlatformTypeModel>(),
                    Publisher = new Publisher(),
                });
            var mockPlatformTypeService = new Mock<IPlatformTypeService>();
            mockPlatformTypeService.Setup(m => m.GetAll())
                .Returns(new List<PlatformTypeModel>());

            GameController gameController = GetGameController(mockGameService,
                mockPlatformTypeService: mockPlatformTypeService);

            const string testKey = "key";

            // Act
            gameController.GetGameDetails(testKey);

            // Assert
            mockGameService.Verify(m => m.GetGameModelByKey(It.IsAny<string>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_New_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Add(It.IsAny<GameModel>())).Verifiable();

            GameController gameController = GetGameController(mockGameService);

            GameViewModel testViewModel = GetGameViewModel();

            // Act
            gameController.AddGame(testViewModel);

            // Assert
            mockGameService.Verify(m => m.Add(It.IsAny<GameModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Update_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Update(It.IsAny<GameModel>())).Verifiable();

            GameController gameController = GetGameController(mockGameService);

            GameViewModel testViewModel = GetGameViewModel();

            // Act
            gameController.UpdateGame(testViewModel);

            // Assert
            mockGameService.Verify(m => m.Update(It.IsAny<GameModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Remove_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Remove(It.IsAny<GameModel>())).Verifiable();

            GameController gameController = GetGameController(mockGameService);

            GameViewModel testViewModel = GetGameViewModel();

            // Act
            gameController.RemoveGame(testViewModel);

            // Assert
            mockGameService.Verify(m => m.Remove(It.IsAny<GameModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Download_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
                .Returns(new GameModel {Name = "gameName"});

            GameController gameController = GetGameController(mockGameService);

            string testKey = "testKey";

            // Act
            ActionResult actionResult = gameController.DownloadGame(testKey);

            // Assert
            Assert.IsTrue(actionResult is FileResult);
        }
    }
}