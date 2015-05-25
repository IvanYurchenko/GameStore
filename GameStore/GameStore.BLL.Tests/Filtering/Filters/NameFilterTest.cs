using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filtering;
using GameStore.BLL.Filtering.Filters;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.BLL.Tests.Filtering.Filters
{
    [TestClass]
    public class NameFilterTest
    {
        [TestMethod]
        public void Check_That_NameFilter_Returns_Right_Delegate()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    GameNamePart = "name",
                }
            };

            var filter = new NameFilter();

            var list = new List<Game>
            {
                new Game
                {
                    Name = "name1"
                },
                new Game
                {
                    Name = "name2"
                },
                new Game
                {
                    Name = "nam"
                },
            };

            // Act
            filter.Execute(container);
            Func<Game, bool> resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }
    }
}