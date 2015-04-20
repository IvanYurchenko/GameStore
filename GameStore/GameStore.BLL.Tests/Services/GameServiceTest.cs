﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.BLL.Tests.Services
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void Check_That_Game_Service_Adds_Game()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Insert(It.IsAny<Game>()));

            var gameService = new GameService(mock.Object);

            //Act
            gameService.Add(new GameModel());

            //Assert
            mock.Verify(m => m.Save());
        }

        [TestMethod]
        public void Check_That_Game_Service_Removes_Right_Game()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()));
            mock.Setup(m => m.GameRepository.Delete(It.IsAny<Game>()));
            mock.Setup(m => m.Save());

            var gameService = new GameService(mock.Object);

            //Act
            gameService.Remove(new GameModel());

            //Assert
            mock.Verify(m => m.Save());
        }

        [TestMethod]
        public void Check_That_Game_Service_Updates_Right_Game()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()));
            mock.Setup(m => m.GameRepository.Update(It.IsAny<Game>()));
            mock.Setup(m => m.Save());

            var gameService = new GameService(mock.Object);

            //Act
            gameService.Update(new GameModel());

            //Assert
            mock.Verify(m => m.Save());
        }


        [TestMethod]
        public void Check_That_Game_Service_Gets_Game_By_Key()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var testList = new List<Game>
            {
                new Game
                {
                    GameId = 1,
                    Key = "testKey"
                }
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()))
                .Returns<string>(p => testList.First(g => g.Key == p));

            var gameService = new GameService(mock.Object);

            var key = "testKey";

            //Act
            var gameModel = gameService.GetGameModelByKey(key);

            //Assert
            Assert.IsTrue(gameModel.GameId == testList.First().GameId && gameModel.Key == testList.First().Key);
        }

        [TestMethod]
        public void Check_That_Game_Service_Gets_Game_By_Id()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var testList = new List<Game>
            {
                new Game
                {
                    GameId = 1,
                    Key = "testKey"
                }
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns<int>(p => testList.First(g => g.GameId == p));

            var gameService = new GameService(mock.Object);

            var testId = 1;

            //Act
            var gameModel = gameService.GetGameModelById(testId);


            //Assert
            Assert.IsTrue(gameModel.GameId == testList.First().GameId && gameModel.Key == testList.First().Key);
        }

        [TestMethod]
        public void Check_That_Game_Service_Gets_All_Games()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var testList = new List<Game>
            {
                new Game
                {
                    GameId = 1,
                    Key = "testKey",
                    Comments = new List<Comment>(),
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>()
                }
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetAll())
                .Returns(testList);

            var gameService = new GameService(mock.Object);

            //Act
            var games = gameService.GetAllGames();

            //Assert
            Assert.IsTrue(testList.Count == games.Count());
        }


        [TestMethod]
        public void Check_That_Game_Service_Gets_Games_By_Genre()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var testGenre = new Genre {GenreId = 1, Name = "testGenre"};
            var testList = new List<Game>
            {
                new Game
                {
                    GameId = 2,
                    Key = "testKey2",
                    Comments = new List<Comment>(),
                    Genres = new List<Genre> {testGenre},
                    PlatformTypes = new List<PlatformType>()
                },
                new Game
                {
                    GameId = 1,
                    Key = "testKey",
                    Comments = new List<Comment>(),
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>()
                }
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetAll())
                .Returns(testList);
            mock.Setup(m => m.GenreRepository.GetById(It.IsAny<int>()))
                .Returns(testGenre);

            var gameService = new GameService(mock.Object);

            //Act
            var games = gameService.GetGamesByGenre(Mapper.Map<GenreModel>(testGenre));

            //Assert
            Assert.IsTrue(games.Count() == 1);
        }


        [TestMethod]
        public void Check_That_Game_Service_Gets_Games_By_PlatformType()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var testPlatformType = new PlatformType {PlatformTypeId = 1, Type = "Mobile"};
            var testList = new List<Game>
            {
                new Game
                {
                    GameId = 2,
                    Key = "testKey2",
                    Comments = new List<Comment>(),
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType> {testPlatformType}
                },
                new Game
                {
                    GameId = 1,
                    Key = "testKey",
                    Comments = new List<Comment>(),
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>()
                }
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetAll())
                .Returns(testList);
            mock.Setup(m => m.PlatformTypeRepository.GetById(It.IsAny<int>()))
                .Returns(testPlatformType);

            var gameService = new GameService(mock.Object);

            //Act
            var games = gameService.GetGamesByPlatformType(Mapper.Map<PlatformTypeModel>(testPlatformType));

            //Assert
            Assert.IsTrue(games.Count() == 1);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Add_Game_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Insert(It.IsAny<Game>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            var gameModel = new GameModel
            {
                GameId = 5,
                Key = "testKey"
            };

            //Act
            gameService.Add(gameModel);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Remove_Game_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Delete(It.IsAny<Game>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            var gameModel = new GameModel
            {
                GameId = 5,
                Key = "testKey"
            };

            //Act
            gameService.Remove(gameModel);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Update_Game_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Update(It.IsAny<Game>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            var gameModel = new GameModel
            {
                GameId = 5,
                Key = "testKey"
            };

            //Act
            gameService.Update(gameModel);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_GameModel_By_Key_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            var testKey = "testKey";

            //Act
            gameService.GetGameModelByKey(testKey);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_GameModel_By_Id_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            //Act
            gameService.GetGameModelById(1);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_All_Games_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetAll())
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            //Act
            gameService.GetAllGames();
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_Games_By_Genre_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GenreRepository.GetById(It.IsAny<int>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            var genreModel = new GenreModel
            {
                GenreId = 1,
                Name = "testGenre"
            };

            //Act
            gameService.GetGamesByGenre(genreModel);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_Games_By_PlatformType_Rethrows_An_Exception()
        {
            //Arrange
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.PlatformTypeRepository.GetById(It.IsAny<int>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            var platformTypeModel = new PlatformTypeModel
            {
                PlatformTypeId = 1,
                Type = "testPlatformType"
            };

            //Act
            gameService.GetGamesByPlatformType(platformTypeModel);
        }
    }
}