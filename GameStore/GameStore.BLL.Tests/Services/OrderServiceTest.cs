using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Entities;
using GameStore.DAL.UnitsOfWork;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.BLL.Tests.Services
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }
        
        #region Positive tests

        [TestMethod]
        public void Check_That_Order_Service_Creates_Order_From_Basket()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.OrderRepository.Insert(It.IsAny<Order>()));

            var basketModel = new BasketModel();
            var orderService = new OrderService(mock.Object);

            // Act
            orderService.CreateOrderFromBasket(basketModel);

            // Assert
            mock.Verify(m => m.OrderRepository.Insert(It.IsAny<Order>()));
            mock.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void Check_That_Order_Service_Gets_Order_Model_By_Session_Key()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns<Expression<Func<Order, bool>>>(x => new List<Order> {new Order()});

            var orderService = new OrderService(mock.Object);

            const string sessionKey = "sessionKey";

            // Act
            orderService.GetOrderModelBySessionKey(sessionKey);

            // Assert
            mock.Verify(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>()));
        }

        [TestMethod]
        public void Check_That_Order_Service_Adds_Basket_Items_To_Order()
        {
            // Arrange
            var basketItems = new List<BasketItemModel>
            {
                new BasketItemModel(),
                new BasketItemModel(),
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns<Expression<Func<Order, bool>>>(x => new List<Order> { new Order {OrderId = 1} });
            mock.Setup(m => m.OrderDetailRepository.Insert(It.IsAny<OrderDetail>()));
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns(new Game());

            var orderService = new OrderService(mock.Object);

            const string sessionKey = "sessionKey";

            // Act
            orderService.AddBasketItemsToOrder(basketItems, sessionKey);

            // Assert
            mock.Verify(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>()));
            mock.Verify(m => m.OrderDetailRepository.Insert(It.IsAny<OrderDetail>()), Times.Exactly(basketItems.Count));
        }

        #endregion

        #region Exception test
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Check_That_Order_Service_Create_Order_From_Basket_Rethrows_Exception()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.OrderRepository.Insert(It.IsAny<Order>()))
                .Callback(() => {throw new Exception();});

            var basketModel = new BasketModel();
            var orderService = new OrderService(mock.Object);

            // Act
            orderService.CreateOrderFromBasket(basketModel);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Check_That_Order_Service_Get_Order_Model_By_Session_Key_Rethrows_Exception()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>()))
                .Callback(() => { throw new Exception(); });

            var orderService = new OrderService(mock.Object);

            const string sessionKey = "sessionKey";

            // Act
            orderService.GetOrderModelBySessionKey(sessionKey);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Check_That_Order_Service_Add_Basket_Items_To_Order_Rethrows_Exception()
        {
            // Arrange
            var basketItems = new List<BasketItemModel>
            {
                new BasketItemModel(),
                new BasketItemModel(),
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns<Expression<Func<Order, bool>>>(x => new List<Order> { new Order { OrderId = 1 } });
            mock.Setup(m => m.OrderDetailRepository.Insert(It.IsAny<OrderDetail>()))
                .Callback(() => { throw new Exception(); }); ;
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns(new Game());

            var orderService = new OrderService(mock.Object);

            const string sessionKey = "sessionKey";

            // Act
            orderService.AddBasketItemsToOrder(basketItems, sessionKey);

            // Assert
            mock.Verify(m => m.OrderRepository.Get(It.IsAny<Expression<Func<Order, bool>>>()));
            mock.Verify(m => m.OrderDetailRepository.Insert(It.IsAny<OrderDetail>()), Times.Exactly(basketItems.Count));
        }

        #endregion
    }
}
