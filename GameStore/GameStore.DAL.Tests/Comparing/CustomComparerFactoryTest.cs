using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Comparing;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.Comparing
{
    [TestClass]
    public class CustomComparerFactoryTest
    {
        [TestMethod]
        public void Check_That_Custom_Comparer_Factory_Returns_Game_Comparer()
        {
            var customComparerFactory = new CustomComparerFactory();

            ICustomComparer comparer = customComparerFactory.GetComparer(typeof(Game));

            Assert.IsInstanceOfType(comparer, typeof(GameComparer));
        }

        [TestMethod]
        public void Check_That_Custom_Comparer_Factory_Returns_Genre_Comparer()
        {
            var customComparerFactory = new CustomComparerFactory();

            ICustomComparer comparer = customComparerFactory.GetComparer(typeof(Genre));

            Assert.IsInstanceOfType(comparer, typeof(GenreComparer));
        }

        [TestMethod]
        public void Check_That_Custom_Comparer_Factory_Returns_Order_Comparer()
        {
            var customComparerFactory = new CustomComparerFactory();

            ICustomComparer comparer = customComparerFactory.GetComparer(typeof(Order));

            Assert.IsInstanceOfType(comparer, typeof(OrderComparer));
        }

        [TestMethod]
        public void Check_That_Custom_Comparer_Factory_Returns_Order_Item_Comparer()
        {
            var customComparerFactory = new CustomComparerFactory();

            ICustomComparer comparer = customComparerFactory.GetComparer(typeof(OrderItem));

            Assert.IsInstanceOfType(comparer, typeof(OrderItemComparer));
        }

        [TestMethod]
        public void Check_That_Custom_Comparer_Factory_Returns_Publisher_Comparer()
        {
            var customComparerFactory = new CustomComparerFactory();

            ICustomComparer comparer = customComparerFactory.GetComparer(typeof(Publisher));

            Assert.IsInstanceOfType(comparer, typeof(PublisherComparer));
        }
    }
}
