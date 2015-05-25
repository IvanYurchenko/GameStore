using System;
using System.Collections.Generic;
using GameStore.Core.Enums;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.Comparing.Concrete
{
    [TestClass]
    public class OrderComparerTest
    {
        [TestMethod]
        public void Check_That_Order_Comparer_Returns_True_For_Equal_Orders()
        {
            var order1 = new Order
            {
                NorthwindId = 1,
                IsReadonly = true,
                OrderItems = new List<OrderItem>(),
                OrderDate = new DateTime(),
                SessionKey = "key",
                OrderStatus = OrderStatus.New,
                OrderId = 5,
            };

            var order2 = new Order
            {
                NorthwindId = 1,
                IsReadonly = true,
                OrderItems = new List<OrderItem>(),
                OrderDate = new DateTime(),
                SessionKey = "key",
                OrderStatus = OrderStatus.New,
                OrderId = 5,
            };

            var comparer = new OrderComparer();

            bool result = comparer.AreEqual(order1, order2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Order_Comparer_Returns_False_For_Not_Equal_Orders()
        {

            var order1 = new Order
            {
                NorthwindId = 1,
                IsReadonly = true,
                OrderItems = new List<OrderItem>(),
                OrderDate = new DateTime(),
                SessionKey = "key",
                OrderStatus = OrderStatus.New,
                OrderId = 5,
            };

            var order2 = new Order
            {
                NorthwindId = 4,
                IsReadonly = true,
                OrderItems = new List<OrderItem>(),
                OrderDate = new DateTime(),
                SessionKey = "key",
                OrderStatus = OrderStatus.New,
                OrderId = 5,
            };

            var comparer = new OrderComparer();

            bool result = comparer.AreEqual(order1, order2);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Order_Comparer_Throws_Exception_When_Obj1_Has_Wrong_Type()
        {
            var comparer = new OrderComparer();

            var obj1 = new Object();
            var obj2 = new Order();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Order_Comparer_Throws_Exception_When_Obj2_Has_Wrong_Type()
        {
            var comparer = new OrderComparer();

            var obj1 = new Order();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Order_Comparer_Throws_Exception_When_Obj1_And_Obj2_Have_Wrong_Type()
        {
            var comparer = new OrderComparer();

            var obj1 = new Object();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }
    
    }
}
