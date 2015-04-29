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
    public class PublisherFilterTest
    {
        [TestMethod]
        public void Check_That_PublisherFilter_Returns_Right_Delegate()
        {
            // Arrange
            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    Publishers = new List<int> {1, 2}
                }
            };

            var filter = new PublisherFilter();

            var list = new List<Game>
            {
                new Game
                {
                    PublisherId = 1,
                },
                new Game
                {
                    PublisherId = 2,
                },
                new Game
                {
                    PublisherId = 3,
                },
            };

            // Act
            filter.Execute(container);
            var resultCondition = CombinePredicate<Game>.CombineWithAnd(container.Conditions);
            IEnumerable<Game> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }
    }
}