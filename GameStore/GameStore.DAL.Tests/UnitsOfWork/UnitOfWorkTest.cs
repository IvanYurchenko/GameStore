using GameStore.DAL.Entities;
using GameStore.DAL.Repositories;
using GameStore.DAL.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.UnitsOfWork
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void Check_That_GameRepository_Property_Returns_Right_Repository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();

            // Act
            var repo = unitOfWork.GameRepository;

            // Assert
            Assert.IsInstanceOfType(repo, typeof (GameRepository));
        }

        [TestMethod]
        public void Check_That_CommentRepository_Property_Returns_Right_Repository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();

            // Act
            var repo = unitOfWork.CommentRepository;

            // Assert
            Assert.IsInstanceOfType(repo, typeof (GenericRepository<Comment>));
        }

        [TestMethod]
        public void Check_That_GenreRepository_Property_Returns_Right_Repository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();

            // Act
            var repo = unitOfWork.GenreRepository;

            // Assert
            Assert.IsInstanceOfType(repo, typeof (GenericRepository<Genre>));
        }

        [TestMethod]
        public void Check_That_PlatformTypeRepository_Property_Returns_Right_Repository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();

            // Act
            var repo = unitOfWork.PlatformTypeRepository;

            // Assert
            Assert.IsInstanceOfType(repo, typeof (GenericRepository<PlatformType>));
        }

        [TestMethod]
        public void Check_That_OrderRepository_Property_Returns_Right_Repository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();

            // Act
            var repo = unitOfWork.OrderRepository;

            // Assert
            Assert.IsInstanceOfType(repo, typeof (GenericRepository<Order>));
        }

        [TestMethod]
        public void Check_That_OrderDetailsRepository_Property_Returns_Right_Repository()
        {
            // Arrange
            var unitOfWork = new UnitOfWork();

            // Act
            var repo = unitOfWork.OrderDetailsRepository;

            // Assert
            Assert.IsInstanceOfType(repo, typeof (GenericRepository<OrderDetails>));
        }
    }
}