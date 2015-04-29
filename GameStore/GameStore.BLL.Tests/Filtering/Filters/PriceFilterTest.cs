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
    public class PriceFilterTest
    {
        [TestMethod]
        public void Check_That_PriceFilter_Returns_Right_Delegate()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    PriceFrom = 5,
                    PriceTo = (decimal) 10.5,
                }
            };

            var filter = new PriceFilter();

            var list = new List<Game>
            {
                new Game
                {
                    Price = (decimal) 4.9,
                },
                new Game
                {
                    Price = 5,
                },
                new Game
                {
                    Price = 7,
                },
                new Game
                {
                    Price = (decimal) 10.5,
                },
                new Game
                {
                    Price = (decimal) 10.6,
                },
            };

            // Act
            filter.Execute(container);
            var resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }
    }
}