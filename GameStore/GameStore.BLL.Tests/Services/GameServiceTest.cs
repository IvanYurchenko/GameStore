using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.BLL.Tests.Services
{
    [TestClass]
    public class GameServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        #region Positive tests

        [TestMethod]
        public void Check_That_Game_Service_Adds_Game()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Insert(It.IsAny<Game>()));

            var gameService = new GameService(mock.Object);

            //Act
            gameService.Add(new GameModel());

            //Assert
            mock.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void Check_That_Game_Service_Removes_Game()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()));
            mock.Setup(m => m.GameRepository.Delete(It.IsAny<Game>()));
            mock.Setup(m => m.SaveChanges());

            var gameService = new GameService(mock.Object);

            //Act
            gameService.Remove(new GameModel());

            //Assert
            mock.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void Check_That_Game_Service_Updates_Game()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()));
            mock.Setup(m => m.GameRepository.Update(It.IsAny<Game>()));
            mock.Setup(m => m.SaveChanges());

            var gameService = new GameService(mock.Object);

            //Act
            gameService.Update(new GameModel());

            //Assert
            mock.Verify(m => m.SaveChanges());
        }


        [TestMethod]
        public void Check_That_Game_Service_Gets_Game_By_Key()
        {
            //Arrange
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

            string key = "testKey";

            //Act
            GameModel gameModel = gameService.GetGameModelByKey(key);

            //Assert
            Assert.IsTrue(gameModel.GameId == testList.First().GameId && gameModel.Key == testList.First().Key);
        }

        [TestMethod]
        public void Check_That_Game_Service_Gets_Game_By_Id()
        {
            //Arrange
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

            int testId = 1;

            //Act
            GameModel gameModel = gameService.GetGameModelById(testId);


            //Assert
            Assert.IsTrue(gameModel.GameId == testList.First().GameId && gameModel.Key == testList.First().Key);
        }

        [TestMethod]
        public void Check_That_Game_Service_Gets_All_Games()
        {
            //Arrange
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
            IEnumerable<GameModel> games = gameService.GetAll();

            //Assert
            Assert.IsTrue(testList.Count == games.Count());
        }

        [TestMethod]
        public void Check_That_Game_Service_Gets_Games_By_Genre()
        {
            //Arrange
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
            IEnumerable<GameModel> games = gameService.GetGamesByGenre(Mapper.Map<GenreModel>(testGenre));

            //Assert
            Assert.IsTrue(games.Count() == 1);
        }


        [TestMethod]
        public void Check_That_Game_Service_Gets_Games_By_PlatformType()
        {
            //Arrange
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
            IEnumerable<GameModel> games = gameService.GetGamesByPlatformType(Mapper.Map<PlatformTypeModel>(testPlatformType));

            //Assert
            Assert.IsTrue(games.Count() == 1);
        }

        [TestMethod]
        public void Check_That_Game_Service_Gets_Games_Count()
        {
            //Arrange
            int testCount = 5;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetCount(It.IsAny<Func<Game, bool>>()))
                .Returns(testCount);

            var gameService = new GameService(mock.Object);

            //Act
            int result = gameService.GetGamesCount();

            //Assert
            Assert.IsTrue(result == testCount);
        }

        [TestMethod]
        public void Check_That_Game_Service_Game_Exists_Returns_True_For_Key_Equals()
        {
            //Arrange
            var games = new List<Game>
            {
                new Game
                {
                    GameId = 1,
                    Key = "key1",
                },
                new Game
                {
                    GameId = 2,
                    Key = "key2",
                },
                new Game
                {
                    GameId = 3,
                    Key = "key3",
                },
            };
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns<Expression<Func<Game, bool>>>(expr => games.Where(expr.Compile()));

            var gameService = new GameService(mock.Object);

            //Act
            bool result = gameService.GameExists("key1", 3);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Game_Service_Game_Exists_Returns_False_For_Key_Not_Equals()
        {
            //Arrange
            var games = new List<Game>
            {
                new Game
                {
                    GameId = 1,
                    Key = "key1",
                },
                new Game
                {
                    GameId = 2,
                    Key = "key2",
                },
                new Game
                {
                    GameId = 3,
                    Key = "key3",
                },
            };
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns<Expression<Func<Game, bool>>>(expr => games.Where(expr.Compile()));

            var gameService = new GameService(mock.Object);

            //Act
            bool result = gameService.GameExists("key5", 3);

            //Assert
            Assert.IsTrue(result == false);
        }

        [TestMethod]
        public void Check_That_Game_Service_Game_Exists_Returns_False_For_Key_And_Id_Equal()
        {
            //Arrange
            var games = new List<Game>
            {
                new Game
                {
                    GameId = 1,
                    Key = "key1",
                },
                new Game
                {
                    GameId = 2,
                    Key = "key2",
                },
                new Game
                {
                    GameId = 3,
                    Key = "key3",
                },
            };
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns<Expression<Func<Game, bool>>>(expr => games.Where(expr.Compile()));

            var gameService = new GameService(mock.Object);

            //Act
            bool result = gameService.GameExists("key1", 1);

            //Assert
            Assert.IsTrue(result == false);
        }

        #endregion

        #region Exception tests

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Add_Game_Rethrows_An_Exception()
        {
            //Arrange
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
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            string testKey = "testKey";

            //Act
            gameService.GetGameModelByKey(testKey);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_GameModel_By_Id_Rethrows_An_Exception()
        {
            //Arrange
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
            gameService.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_Games_By_Genre_Rethrows_An_Exception()
        {
            //Arrange
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

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Get_Games_Count_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetCount(It.IsAny<Func<Game, bool>>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            //Act
            gameService.GetGamesCount();
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Game_Service_Game_Exists_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Callback(() => { throw new Exception(); });

            var gameService = new GameService(mock.Object);

            const string testKey = "key";
            const int testId = 5;

            //Act
            gameService.GameExists(testKey, testId);
        }

        #endregion
    }
}