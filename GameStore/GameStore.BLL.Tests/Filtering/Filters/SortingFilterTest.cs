using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Filtering;
using GameStore.BLL.Filtering.Filters;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.BLL.Tests.Filtering.Filters
{
    [TestClass]
    public class SortingFilterTest
    {
        #region Helpers

        private IEnumerable<Game> GetGamesList()
        {
            var gamesList = new List<Game>
            {
                new Game
                {
                    Name = "game1",
                    Price = 1,
                    AddedDate = DateTime.UtcNow.AddDays(-10),
                    Comments = new List<Comment>
                    {
                        new Comment(),
                    },
                },
                new Game
                {
                    Name = "game2",
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
                    Name = "game3",
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
                    Name = "game4",
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

        #endregion

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

            var list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().Name == "game1");
            Assert.IsTrue(result.Last().Name == "game4");
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

            var list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().Name == "game4");
            Assert.IsTrue(result.Last().Name == "game1");
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

            var list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().Name == "game3");
            Assert.IsTrue(result.Last().Name == "game4");
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

            var list = GetGamesList();

            // Act
            filter.Execute(container);
            IEnumerable<Game> result = list.OrderBy(x => (container.SortCondition(x)));

            // Assert
            Assert.IsTrue(result.First().Name == "game4");
            Assert.IsTrue(result.Last().Name == "game1");
        }
    }
}