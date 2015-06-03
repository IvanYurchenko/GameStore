using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Filtering;
using GameStore.BLL.Filtering.Filters;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.BLL.Tests.Filtering.Filters
{
    [TestClass]
    public class SortingFilterTest
    {
        private static IEnumerable<Game> GetGamesList()
        {
            var gamesList = new List<Game>
            {
                new Game
                {
                    GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Name = "game1",
                            Language = new Language
                            {
                                Code = "en",
                                LanguageId = 1,
                            }
                        }
                    },
                    Price = 1,
                    AddedDate = DateTime.UtcNow.AddDays(-10),
                    Comments = new List<Comment>
                    {
                        new Comment(),
                    },
                },
                new Game
                {
                    GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Name = "game2",
                            Language = new Language
                            {
                                Code = "en",
                                LanguageId = 1,
                            }
                        }
                    },
                    Price = 2,
                    AddedDate = DateTime.UtcNow.AddDays(-5),
                    Comments = new List<Comment>
                    {
                        new Comment(),
                        new Comment(),
                        new Comment(),
                    },
                },
                new Game
                {
                    GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Name = "game3",
                            Language = new Language
                            {
                                Code = "en",
                                LanguageId = 1,
                            }
                        }
                    },
                    Price = 5,
                    AddedDate = DateTime.UtcNow.AddDays(0),
                    Comments = new List<Comment>
                    {
                        new Comment(),
                        new Comment(),
                    },
                },
                new Game
                {
                    GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Name = "game4",
                            Language = new Language
                            {
                                Code = "en",
                                LanguageId = 1,
                            }
                        }
                    },
                    Price = 10,
                    AddedDate = DateTime.UtcNow.AddDays(-20),
                    Comments = new List<Comment>
                    {
                        new Comment(),
                        new Comment(),
                        new Comment(),
                        new Comment(),
                        new Comment(),
                    },
                },
            };

            return gamesList;
        }

        [TestMethod]
        public void Check_That_SortFilter_Returns_Right_Sorting_Order_For_PriceAscending()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    SortCondition = SortCondition.PriceAscending,
                }
            };

            var filter = new SortingFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().GameLocalizations.First().Name == "game1");
            Assert.IsTrue(result.Last().GameLocalizations.First().Name == "game4");
        }

        [TestMethod]
        public void Check_That_SortFilter_Returns_Right_Sorting_Order_For_PriceDescending()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    SortCondition = SortCondition.PriceDescending,
                }
            };

            var filter = new SortingFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().GameLocalizations.First().Name == "game4");
            Assert.IsTrue(result.Last().GameLocalizations.First().Name == "game1");
        }

        [TestMethod]
        public void Check_That_SortFilter_Returns_Right_Sorting_Order_For_Newest()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    SortCondition = SortCondition.Newest,
                }
            };

            var filter = new SortingFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().GameLocalizations.First().Name == "game3");
            Assert.IsTrue(result.Last().GameLocalizations.First().Name == "game4");
        }

        [TestMethod]
        public void Check_That_SortFilter_Returns_Right_Sorting_Order_For_MostCommented()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    SortCondition = SortCondition.MostCommented,
                }
            };

            var filter = new SortingFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().GameLocalizations.First().Name == "game4");
            Assert.IsTrue(result.Last().GameLocalizations.First().Name == "game1");
        }
    }
}