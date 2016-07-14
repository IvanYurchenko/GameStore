using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Localization;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Mappings;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.Localization;
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

        private static GameController GetGameController(
            IMock<IGameService> mockGameService = null,
            IMock<IGenreService> mockGenreService = null,
            IMock<IPlatformTypeService> mockPlatformTypeService = null,
            IMock<IPublisherService> mockPublisherService = null,
            IMock<ILanguageService> mockLanguageService = null
            )
        {
            mockGameService = mockGameService ?? new Mock<IGameService>();
            mockGenreService = mockGenreService ?? new Mock<IGenreService>();
            mockPlatformTypeService = mockPlatformTypeService ?? new Mock<IPlatformTypeService>();
            mockPublisherService = mockPublisherService ?? new Mock<IPublisherService>();
            mockLanguageService = mockLanguageService ?? new Mock<ILanguageService>();

            var gameController = new GameController(
                mockGameService.Object,
                mockGenreService.Object,
                mockPlatformTypeService.Object,
                mockPublisherService.Object,
                mockLanguageService.Object);

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
                Publisher = new PublisherViewModel(),
                SelectedGenresIds = new List<int>(),
                SelectedPlatformTypesIds = new List<int>(),
                SelectedPublisherId = 1,
                UnitsInStock = 50,
                AddedDate = DateTime.UtcNow,
                PublicationDate = DateTime.UtcNow,
                Publishers = new List<PublisherViewModel>(),
                PlatformTypes = new List<PlatformTypeViewModel>(),
                Genres = new List<GenreViewModel>(),
            };

            return gameViewModel;
        }

        private static GameAddUpdateViewModel GetGameAddUpdateViewModel()
        {
            var gameViewModel = new GameAddUpdateViewModel
            {
                GameId = 1,
                Key = "key",
                Price = (decimal)5.0,
                Discontinued = false,
                Publisher = new PublisherViewModel(),
                SelectedGenresIds = new List<int>(),
                SelectedPlatformTypesIds = new List<int>(),
                SelectedPublisherId = 1,
                UnitsInStock = 50,
                AddedDate = DateTime.UtcNow,
                PublicationDate = DateTime.UtcNow,
                Publishers = new List<PublisherViewModel>(),
                PlatformTypes = new List<PlatformTypeViewModel>(),
                Genres = new List<GenreViewModel>(),
                GameLocalizations = new List<GameLocalizationViewModel>
                {
                    new GameLocalizationViewModel
                    {
                        Name = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new LanguageViewModel
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
            };

            return gameViewModel;
        }

        private static GameModel GetGameModel()
        {
            var gameViewModel = new GameModel
            {
                GameId = 1,
                Key = "key",
                Price = (decimal)5.0,
                Discontinued = false,
                Publisher = new PublisherModel
                {
                    PublisherLocalizations = new List<PublisherLocalizationModel>
                {
                    new PublisherLocalizationModel
                    {
                        CompanyName = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new LanguageModel
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
                },
                UnitsInStock = 50,
                AddedDate = DateTime.UtcNow,
                PublicationDate = DateTime.UtcNow,
                PlatformTypes = new List<PlatformTypeModel>(),
                Genres = new List<GenreModel>(),
                GameLocalizations = new List<GameLocalizationModel>
                {
                    new GameLocalizationModel
                    {
                        Name = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new LanguageModel
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
            };

            return gameViewModel;
        }

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
                .Returns(GetGameModel);
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

            GameAddUpdateViewModel testAddUpdateViewModel = GetGameAddUpdateViewModel();

            // Act
            gameController.AddGame(testAddUpdateViewModel);

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

            GameAddUpdateViewModel testAddUpdateViewModel = GetGameAddUpdateViewModel();

            // Act
            gameController.UpdateGame(testAddUpdateViewModel);

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

            const string key = "key";

            // Act
            gameController.RemoveGame(key);

            // Assert
            mockGameService.Verify(m => m.Remove(It.IsAny<GameModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Download_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
                .Returns(new GameModel {GameLocalizations = new List<GameLocalizationModel>
                {
                    new GameLocalizationModel
                    {
                        Name = "name",
                        Description = "descr",
                        Language = new LanguageModel
                        {
                            Code = "en",
                            LanguageId = 1,
                            Name = "English",
                        }
                    }
                }});

            GameController gameController = GetGameController(mockGameService);

            string testKey = "testKey";

            // Act
            ActionResult actionResult = gameController.DownloadGame(testKey);

            // Assert
            Assert.IsTrue(actionResult is FileResult);
        }
    }
}