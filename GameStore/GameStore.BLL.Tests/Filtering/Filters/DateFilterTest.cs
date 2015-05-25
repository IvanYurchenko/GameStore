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
    public class DateFilterTest
    {
        #region Helpers

        private static IEnumerable<Game> GetGamesList()
        {
            var gamesList = new List<Game>
            {
                new Game
                {
                    PublicationDate = DateTime.UtcNow.AddDays(-3),
                },
                new Game
                {
                    PublicationDate = DateTime.UtcNow.AddDays(-20),
                },
                new Game
                {
                    PublicationDate = DateTime.UtcNow.AddDays(-50),
                },
                new Game
                {
                    PublicationDate = DateTime.UtcNow.AddDays(-400),
                },
                new Game
                {
                    PublicationDate = DateTime.UtcNow.AddDays(-800),
                },
                new Game
                {
                    PublicationDate = DateTime.UtcNow.AddDays(-1200),
                },
                new Game
                {
                    PublicationDate = DateTime.UtcNow.AddDays(-2000),
                },
            };

            return gamesList;
        }

        #endregion

        [TestMethod]
        public void Check_That_DateFilter_Returns_Right_Delegate_By_LastWeek()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    DatePeriod = DatePeriod.LastWeek,
                }
            };

            var filter = new DateFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            Func<Game, bool> resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 1);
        }

        [TestMethod]
        public void Check_That_DateFilter_Returns_Right_Delegate_By_LastMonth()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    DatePeriod = DatePeriod.LastMonth,
                }
            };

            var filter = new DateFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            Func<Game, bool> resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void Check_That_DateFilter_Returns_Right_Delegate_By_LastYear()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    DatePeriod = DatePeriod.LastYear,
                }
            };

            var filter = new DateFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            Func<Game, bool> resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }

        [TestMethod]
        public void Check_That_DateFilter_Returns_Right_Delegate_By_TwoYears()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    DatePeriod = DatePeriod.TwoYears,
                }
            };

            var filter = new DateFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            Func<Game, bool> resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 4);
        }

        [TestMethod]
        public void Check_That_DateFilter_Returns_Right_Delegate_By_ThreeYears()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    DatePeriod = DatePeriod.ThreeYears,
                }
            };

            var filter = new DateFilter();

            IEnumerable<Game> list = GetGamesList();

            // Act
            filter.Execute(container);
            Func<Game, bool> resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 5);
        }
    }
}