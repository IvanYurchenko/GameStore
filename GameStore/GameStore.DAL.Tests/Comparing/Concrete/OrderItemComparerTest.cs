using System;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.Comparing.Concrete
{
    [TestClass]
    public class OrderItemComparerTest
    {
        [TestMethod]
        public void Check_That_OrderItem_Comparer_Returns_True_For_Equal_OrderItems()
        {
            var orderItem1 = new OrderItem
            {
                IsReadonly = true,
                Game = new Game(),
                OrderId = 5,
                Price = 5,
                GameId = 5,
                Order = new Order(),
                Discount = (decimal) 0.4,
                NorthwindOrderId = 5,
                NorthwindProductId = 4,
                OrderItemId = 5,
                Quantity = 5,
            };

            var orderItem2 = new OrderItem
            {
                IsReadonly = true,
                Game = new Game(),
                OrderId = 5,
                Price = 5,
                GameId = 5,
                Order = new Order(),
                Discount = (decimal)0.4,
                NorthwindOrderId = 5,
                NorthwindProductId = 4,
                OrderItemId = 5,
                Quantity = 5,
            };

            var comparer = new OrderItemComparer();

            bool result = comparer.AreEqual(orderItem1, orderItem2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_OrderItem_Comparer_Returns_False_For_Not_Equal_OrderItems()
        {
            var orderItem1 = new OrderItem
            {
                IsReadonly = true,
                Game = new Game(),
                OrderId = 55,
                Price = 5,
                GameId = 5,
                Order = new Order(),
                Discount = (decimal)0.4,
                NorthwindOrderId = 5,
                NorthwindProductId = 4,
                OrderItemId = 5,
                Quantity = 5,
            };

            var orderItem2 = new OrderItem
            {
                IsReadonly = true,
                Game = new Game(),
                OrderId = 5,
                Price = 5,
                GameId = 5,
                Order = new Order(),
                Discount = (decimal)0.4,
                NorthwindOrderId = 5,
                NorthwindProductId = 4,
                OrderItemId = 5,
                Quantity = 5,
            };

            var comparer = new OrderItemComparer();

            bool result = comparer.AreEqual(orderItem1, orderItem2);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_OrderItem_Comparer_Throws_Exception_When_Obj1_Has_Wrong_Type()
        {
            var comparer = new OrderItemComparer();

            var obj1 = new Object();
            var obj2 = new OrderItem();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_OrderItem_Comparer_Throws_Exception_When_Obj2_Has_Wrong_Type()
        {
            var comparer = new OrderItemComparer();

            var obj1 = new OrderItem();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_OrderItem_Comparer_Throws_Exception_When_Obj1_And_Obj2_Have_Wrong_Type()
        {
            var comparer = new OrderItemComparer();

            var obj1 = new Object();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }
    
    }
}
