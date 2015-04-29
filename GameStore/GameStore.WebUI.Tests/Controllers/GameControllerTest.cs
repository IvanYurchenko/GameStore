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

        private GameViewModel GetGameViewModel()
        {
            var gameViewModel = new GameViewModel
            {
                GameId = 1,
                Key = "key",
                Price = (decimal)5.0,
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
            var mockCommentService = new Mock<ICommentService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockPlatformTypeService = new Mock<IPlatformTypeService>();
            var mockLogger = new Mock<ILogger>();
            var mockBasketService = new Mock<IBasketService>();
            var mockPublisherService = new Mock<IPublisherService>();

            var gamesController = new GameController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockBasketService.Object,
                mockPublisherService.Object,
                mockLogger.Object);

            // Act
            gamesController.GetGames(new GameIndexViewModel());

            // Assert
            mockGameService.Verify(m => m.GetGamesByFilter(It.IsAny<GamesFilterModel>(), It.IsAny<PaginationModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Details_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
                .Returns(new GameModel()
                {
                    Genres = new List<GenreModel>(),
                    PlatformTypes = new List<PlatformTypeModel>(),
                    Publisher = new Publisher(),
                });
            var mockCommentService = new Mock<ICommentService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockPlatformTypeService = new Mock<IPlatformTypeService>();
            mockPlatformTypeService.Setup(m => m.GetAll())
                .Returns(new List<PlatformTypeModel>());
            var mockLogger = new Mock<ILogger>();
            var mockBasketService = new Mock<IBasketService>();
            var mockPublisherService = new Mock<IPublisherService>();

            var gamesController = new GameController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockBasketService.Object,
                mockPublisherService.Object,
                mockLogger.Object);

            const string testKey = "key";

            // Act
            gamesController.GetGameDetails(testKey);

            // Assert
            mockGameService.Verify(m => m.GetGameModelByKey(It.IsAny<string>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_New_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Add(It.IsAny<GameModel>())).Verifiable();
            var mockCommentService = new Mock<ICommentService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockPlatformTypeService = new Mock<IPlatformTypeService>();
            var mockLogger = new Mock<ILogger>();
            var mockBasketService = new Mock<IBasketService>();
            var mockPublisherService = new Mock<IPublisherService>();

            var gamesController = new GameController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockBasketService.Object,
                mockPublisherService.Object,
                mockLogger.Object);

            GameViewModel testViewModel = GetGameViewModel();

            // Act
            gamesController.AddGame(testViewModel);

            // Assert
            mockGameService.Verify(m => m.Add(It.IsAny<GameModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Update_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Update(It.IsAny<GameModel>())).Verifiable();
            var mockCommentService = new Mock<ICommentService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockPlatformTypeService = new Mock<IPlatformTypeService>();
            var mockLogger = new Mock<ILogger>();
            var mockBasketService = new Mock<IBasketService>();
            var mockPublisherService = new Mock<IPublisherService>();

            var gamesController = new GameController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockBasketService.Object,
                mockPublisherService.Object,
                mockLogger.Object);

            var testViewModel = GetGameViewModel();

            // Act
            gamesController.UpdateGame(testViewModel);

            // Assert
            mockGameService.Verify(m => m.Update(It.IsAny<GameModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Remove_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.Remove(It.IsAny<GameModel>())).Verifiable();
            var mockCommentService = new Mock<ICommentService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockPlatformTypeService = new Mock<IPlatformTypeService>();
            var mockLogger = new Mock<ILogger>();
            var mockBasketService = new Mock<IBasketService>();
            var mockPublisherService = new Mock<IPublisherService>();

            var gamesController = new GameController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockBasketService.Object,
                mockPublisherService.Object,
                mockLogger.Object);

            var testViewModel = GetGameViewModel();

            // Act
            gamesController.RemoveGame(testViewModel);

            // Assert
            mockGameService.Verify(m => m.Remove(It.IsAny<GameModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Download_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
                .Returns(new GameModel() {Name = "gameName"});
            var mockCommentService = new Mock<ICommentService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockPlatformTypeService = new Mock<IPlatformTypeService>();
            var mockLogger = new Mock<ILogger>();
            var mockBasketService = new Mock<IBasketService>();
            var mockPublisherService = new Mock<IPublisherService>();

            var gamesController = new GameController(
                mockGameService.Object,
                mockCommentService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockBasketService.Object,
                mockPublisherService.Object,
                mockLogger.Object);

            var testKey = "testKey";

            // Act
            var actionResult = gamesController.DownloadGame(testKey);

            // Assert
            Assert.IsTrue(actionResult is FileResult);
        }
    }
}