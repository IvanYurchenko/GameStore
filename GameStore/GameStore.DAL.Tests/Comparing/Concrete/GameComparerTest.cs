using System;
using System.Collections.Generic;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.Comparing.Concrete
{
    [TestClass]
    public class GameComparerTest
    {
        [TestMethod]
        public void Check_That_Game_Comparer_Returns_True_For_Equal_Games()
        {
            var game1 = new Game
            {
                GameId = 1,
                NorthwindId = 1,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Description = "descr",
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                Name = "name",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher(),
                PublisherId = 5,
                UnitsInStock = 6,
            };

            var game2 = new Game
            {
                GameId = 1,
                NorthwindId = 1,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Description = "descr",
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                Name = "name",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher(),
                PublisherId = 5,
                UnitsInStock = 6,
            };

            var comparer = new GameComparer();

            bool result = comparer.AreEqual(game1, game2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Game_Comparer_Returns_False_For_Not_Equal_Games()
        {
            var game1 = new Game
            {
                GameId = 1,
                NorthwindId = 2,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Description = "descr",
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                Name = "name",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher(),
                PublisherId = 5,
                UnitsInStock = 6,
            };

            var game2 = new Game
            {
                GameId = 1,
                NorthwindId = 1,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Description = "descr",
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                Name = "name",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher(),
                PublisherId = 5,
                UnitsInStock = 6,
            };

            var comparer = new GameComparer();

            bool result = comparer.AreEqual(game1, game2);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Game_Comparer_Throws_Exception_When_Obj1_Has_Wrong_Type()
        {
            var comparer = new GameComparer();

            var obj1 = new Object();
            var obj2 = new Game();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Game_Comparer_Throws_Exception_When_Obj2_Has_Wrong_Type()
        {
            var comparer = new GameComparer();

            var obj1 = new Game();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Game_Comparer_Throws_Exception_When_Obj1_And_Obj2_Have_Wrong_Type()
        {
            var comparer = new GameComparer();

            var obj1 = new Object();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }
    
    }
}
