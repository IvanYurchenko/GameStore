using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.ServiceLayer;
using DAL.Entities;
using DAL.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MVCTask1.UnitTests
{
    [TestClass]
    class GamesControllerTest
    {
        [TestMethod]
        public void CreateValidGame()
        {
            //Arrange
            var gameRepo = new List<Game>();
            var gameDTO = new GameDTO()
            {
                GameId = 10,
                Key = "someKey",
                Name = "someName"
            };
            var game= Mapper.Map<Game>(gameDTO);
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.Setup(uow => uow.GameRepository.Insert(It.IsAny<Game>())).
                    Callback<Game>(x => gameRepo.Add(x));
            var gameServices = new GameService(mockUoW.Object);

            //Act
            gameServices.Add(gameDTO);

            //Assert
            Assert.IsTrue(gameRepo.Count(x => x.GameId == game.GameId) == 1);
        }

    }
}
