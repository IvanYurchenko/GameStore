using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Filtering;
using GameStore.BLL.Filtering.Filters;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
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
                    GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Name = "name1",
                            Language = new Language
                            {
                                Code = "en",
                                LanguageId = 1,
                            }
                        }
                    },
                },
                new Game
                {
                   GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Name = "name2",
                            Language = new Language
                            {
                                Code = "en",
                                LanguageId = 1,
                            }
                        }
                    },
                },
                new Game
                {
                    GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Name = "nam",
                            Language = new Language
                            {
                                Code = "en",
                                LanguageId = 1,
                            }
                        }
                    },
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