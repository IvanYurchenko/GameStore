using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Filtering;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.BLL.Tests.Filtering
{
    [TestClass]
    public class CombinePredicateTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Check_That_CombinePredicate_Combines_Predicates_With_And_Properly()
        {
            // Arrange
            var list = new List<int> {1, 2, 3, 4, 5};

            var conditions = new List<Func<int, bool>>
            {
                x => x < 4,
                x => x > 1,
            };

            // Act
            Func<int, bool> resultCondition = CombinePredicate<int>.CombineWithAnd(conditions);
            IEnumerable<int> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void Check_That_CombinePredicate_Combines_Predicates_With_Or_Properly()
        {
            // Arrange
            var list = new List<int> {1, 2, 3, 4, 5};

            var conditions = new List<Func<int, bool>>
            {
                x => x < 2,
                x => x > 3,
            };

            // Act
            Func<int, bool> resultCondition = CombinePredicate<int>.CombineWithOr(conditions);
            IEnumerable<int> result = list.Where(x => (resultCondition(x)));

            // Assert
            Assert.IsTrue(result.Count() == 3);
        }
    }
}