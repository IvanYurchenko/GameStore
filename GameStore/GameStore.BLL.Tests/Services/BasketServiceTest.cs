using System;
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
    public class BasketServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        #region Helpers

        private static BasketItemModel GetBasketItemModel()
        {
            var basketItemModel = new BasketItemModel
            {
                BasketItemId = 1,
                GameId = 1,
                BasketId = 1,
                Price = 5,
                Quantity = 10,
            };

            return basketItemModel;
        }

        #endregion

        #region Positive tests

        [TestMethod]
        public void Check_That_Basket_Service_Adds_BasketItem()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns(new Game());
            mock.Setup(m => m.BasketItemRepository.Insert(It.IsAny<BasketItem>()));

            var basketItemModel = GetBasketItemModel();
            var basketService = new BasketService(mock.Object);

            // Act
            basketService.AddBasketItem(basketItemModel);

            // Assert
            mock.Verify(m => m.BasketItemRepository.Insert(It.IsAny<BasketItem>()));
            mock.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void Check_That_Basket_Service_Updates_BasketItem()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns(new Game());
            mock.Setup(m => m.BasketItemRepository.Update(It.IsAny<BasketItem>()));

            var basketItemModel = GetBasketItemModel();
            var basketService = new BasketService(mock.Object);

            // Act
            basketService.UpdateBasketItem(basketItemModel);

            // Assert
            mock.Verify(m => m.BasketItemRepository.Update(It.IsAny<BasketItem>()));
            mock.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void Check_That_Basket_Service_Removes_BasketItem()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.BasketItemRepository.Delete(It.IsAny<int>()));

            const int id = 1;
            var basketService = new BasketService(mock.Object);

            // Act
            basketService.RemoveBasketItem(id);

            // Assert
            mock.Verify(m => m.BasketItemRepository.Delete(It.IsAny<int>()));
            mock.Verify(m => m.SaveChanges());
        }

        #endregion

        #region Exception tests

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Basket_Service_AddBasketItem_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns(new Game());

            mock.Setup(m => m.BasketItemRepository.Insert(It.IsAny<BasketItem>()))
                .Callback(() => { throw new Exception(); });

            var basketItemModel = GetBasketItemModel();
            var basketService = new BasketService(mock.Object);

            //Act
            basketService.AddBasketItem(basketItemModel);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Basket_Service_UpdateBasketItem_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns(new Game());

            mock.Setup(m => m.BasketItemRepository.Update(It.IsAny<BasketItem>()))
                .Callback(() => { throw new Exception(); });

            var basketItemModel = GetBasketItemModel();
            var basketService = new BasketService(mock.Object);

            //Act
            basketService.UpdateBasketItem(basketItemModel);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Basket_Service_RemoveBasketItem_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetById(It.IsAny<int>()))
                .Returns(new Game());

            mock.Setup(m => m.BasketItemRepository.Delete(It.IsAny<int>()))
                .Callback(() => { throw new Exception(); });

            const int id = 1;
            var basketService = new BasketService(mock.Object);

            //Act
            basketService.RemoveBasketItem(id);
        }

        #endregion
    }
}