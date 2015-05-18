using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.BLL.Tests.Services
{
    [TestClass]
    public class GenreServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Check_That_Genre_Service_Gets_All_Genres()
        {
            //Arrange
            var testList = new List<Genre>
            {
                new Genre(),
                new Genre(),
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GenreRepository.GetAll())
                .Returns(testList);

            var genreService = new GenreService(mock.Object);

            //Act
            IEnumerable<GenreModel> genres = genreService.GetAll();

            //Assert
            Assert.IsTrue(testList.Count == genres.Count());
        }
    }
}