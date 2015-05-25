using System;
using System.Collections.Generic;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.Comparing.Concrete
{
    [TestClass]
    public class PublisherComparerTest
    {
        [TestMethod]
        public void Check_That_Publisher_Comparer_Returns_True_For_Equal_Publishers()
        {
            var publisher1 = new Publisher
            {
                IsReadonly = true,
                NorthwindId = 5,
                Description = "sdf",
                Games = new List<Game>(),
                PublisherId = 5,
                CompanyName = "name",
                HomePage = "ww.com",
            };

            var publisher2 = new Publisher
            {
                IsReadonly = true,
                NorthwindId = 5,
                Description = "sdf",
                Games = new List<Game>(),
                PublisherId = 5,
                CompanyName = "name",
                HomePage = "ww.com",
            };

            var comparer = new PublisherComparer();

            bool result = comparer.AreEqual(publisher1, publisher2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Publisher_Comparer_Returns_False_For_Not_Equal_Publishers()
        {
            var publisher1 = new Publisher
            {
                IsReadonly = true,
                NorthwindId = 5,
                Description = "sdf",
                Games = new List<Game>(),
                PublisherId = 5,
                CompanyName = "name",
                HomePage = "ww.com",
            };

            var publisher2 = new Publisher
            {
                IsReadonly = true,
                NorthwindId = 5,
                Description = "sdf",
                Games = new List<Game>(),
                PublisherId = 5,
                CompanyName = "nadme",
                HomePage = "ww.com",
            };

            var comparer = new PublisherComparer();

            bool result = comparer.AreEqual(publisher1, publisher2);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Publisher_Comparer_Throws_Exception_When_Obj1_Has_Wrong_Type()
        {
            var comparer = new PublisherComparer();

            var obj1 = new Object();
            var obj2 = new Publisher();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Publisher_Comparer_Throws_Exception_When_Obj2_Has_Wrong_Type()
        {
            var comparer = new PublisherComparer();

            var obj1 = new Publisher();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Publisher_Comparer_Throws_Exception_When_Obj1_And_Obj2_Have_Wrong_Type()
        {
            var comparer = new PublisherComparer();

            var obj1 = new Object();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }
    
    }
}
