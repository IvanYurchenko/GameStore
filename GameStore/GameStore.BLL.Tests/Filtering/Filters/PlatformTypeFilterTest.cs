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
    public class PlatformTypeFilterTest
    {
        [TestMethod]
        public void Check_That_PlatformTypeFilter_Returns_Right_Delegate()
        {
            // Arrange
            var platformTypes = new List<PlatformType>
            {
                new PlatformType
                {
                    PlatformTypeId = 1,
                    Type = "type1",
                },
                new PlatformType
                {
                    PlatformTypeId = 2,
                    Type = "type2",
                },
                new PlatformType
                {
                    PlatformTypeId = 3,
                    Type = "type3",
                }
            };

            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    PlatformTypes = new List<int> {1, 2}
                }
            };

            var filter = new PlatformTypeFilter();

            var list = new List<Game>
            {
                new Game
                {
                    PlatformTypes = new List<PlatformType>
                    {
                        platformTypes[0],
                        platformTypes[1],
                    }
                },
                new Game
                {
                    PlatformTypes = new List<PlatformType>
                    {
                        platformTypes[1],
                        platformTypes[2],
                    }
                },
                new Game
                {
                    PlatformTypes = new List<PlatformType>
                    {
                        platformTypes[2],
                    }
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