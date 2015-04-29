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
    public class GenreFilterTest
    {
        [TestMethod]
        public void Check_That_GenreFilter_Returns_Right_Delegate()
        {
            // Arrange
            var genres = new List<Genre>
            {
                new Genre {GenreId = 1},
                new Genre {GenreId = 2},
                new Genre {GenreId = 3},
                new Genre {GenreId = 4},
            };

            var container = new GameFilterContainer
            {
                Model = new GamesFilterModel
                {
                    Genres = new List<int> {1, 2}
                }
            };

            var filter = new GenreFilter();

            var list = new List<Game>
            {
                new Game
                {
                    Genres = new List<Genre>
                    {
                        genres[0],
                        genres[1],
                    }
                },
                new Game
                {
                    Genres = new List<Genre>
                    {
                        genres[1],
                        genres[2],
                    }
                },
                new Game
                {
                    Genres = new List<Genre>
                    {
                        genres[2],
                        genres[3],
                    }
                },
                new Game
                {
                    Genres = new List<Genre>
                    {
                        genres[0],
                        genres[3],
                    }
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