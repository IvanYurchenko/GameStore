using System;
using System.Collections.Generic;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.Comparing.Concrete
{
    [TestClass]
    public class GenreComparerTest
    {
        [TestMethod]
        public void Check_That_Genre_Comparer_Returns_True_For_Equal_Genres()
        {
            var genre1 = new Genre
            {
                NorthwindId = 1,
                IsReadonly = true,
                Name = "name",
                Games = new List<Game>(),
                GenreId = 1,
                ParentGenre = new Genre(),
                ParentGenreId = 5,
            };

            var genre2 = new Genre
            {
                NorthwindId = 1,
                IsReadonly = true,
                Name = "name",
                Games = new List<Game>(),
                GenreId = 1,
                ParentGenre = new Genre(),
                ParentGenreId = 5,
            };

            var comparer = new GenreComparer();

            bool result = comparer.AreEqual(genre1, genre2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Genre_Comparer_Returns_False_For_Not_Equal_Genres()
        {
            var genre1 = new Genre
            {
                NorthwindId = 1,
                IsReadonly = true,
                Name = "name",
                Games = new List<Game>(),
                GenreId = 1,
                ParentGenre = new Genre(),
                ParentGenreId = 5,
            };

            var genre2 = new Genre
            {
                NorthwindId = 1,
                IsReadonly = true,
                Name = "name2",
                Games = new List<Game>(),
                GenreId = 5,
                ParentGenre = new Genre(),
                ParentGenreId = 5,
            };

            var comparer = new GenreComparer();

            bool result = comparer.AreEqual(genre1, genre2);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Genre_Comparer_Throws_Exception_When_Obj1_Has_Wrong_Type()
        {
            var comparer = new GameComparer();

            var obj1 = new Object();
            var obj2 = new Genre();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Genre_Comparer_Throws_Exception_When_Obj2_Has_Wrong_Type()
        {
            var comparer = new GenreComparer();

            var obj1 = new Genre();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Genre_Comparer_Throws_Exception_When_Obj1_And_Obj2_Have_Wrong_Type()
        {
            var comparer = new GenreComparer();

            var obj1 = new Object();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }
    
    }
}
