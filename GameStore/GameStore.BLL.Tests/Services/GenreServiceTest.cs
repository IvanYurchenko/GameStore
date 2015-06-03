using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
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

        private static GenreModel GetGenreModel()
        {
            var genreModel = new GenreModel
            {
                GenreId = 1,
                IsReadonly = false,
            };

            return genreModel;
        }

        private static Genre GetGenre()
        {
            var genre = new Genre
            {
                GenreId = 1,
                IsReadonly = false,
            };

            return genre;
        }

        [TestMethod]
        public void Check_That_Genre_Service_Adds_Genre()
        {
            // A
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GenreRepository.Insert(It.IsAny<Genre>()));

            var genreService = new GenreService(mock.Object);

            GenreModel genreModel = GetGenreModel();

            // A
            genreService.Add(genreModel);

            // A
            mock.Verify(m => m.GenreRepository.Insert(It.IsAny<Genre>()));
        }

        [TestMethod]
        public void Check_That_Genre_Service_Gets_Model_By_Id()
        {
            // A
            var mock = new Mock<IUnitOfWork>();
            Genre genre = GetGenre();
            mock.Setup(m => m.GenreRepository.Get(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns<Expression<Func<Genre, bool>>>(expr => new List<Genre> { genre });

            var genreService = new GenreService(mock.Object);

            const int genreId = 1;

            // A
            genreService.GetModelById(genreId);

            // A
            mock.Verify(m => m.GenreRepository.Get(It.IsAny<Expression<Func<Genre, bool>>>()));
        }

        [TestMethod]
        public void Check_That_Order_Service_Updates_Genre()
        {
            // Arrange
            Genre genre = GetGenre();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GenreRepository.Get(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns<Expression<Func<Genre, bool>>>(expr => new List<Genre> { genre });
            mock.Setup(m => m.GenreLocalizationRepository.Delete(It.IsAny<GenreLocalization>()));
            
            mock.Setup(m => m.GenreRepository.Update(It.IsAny<Genre>())).Verifiable();

            var genreService = new GenreService(mock.Object);

            GenreModel genreModel = GetGenreModel();

            // Act
            genreService.Update(genreModel);

            // Assert
            mock.Verify(m => m.GenreRepository.Update(It.IsAny<Genre>()));
        }

        [TestMethod]
        public void Check_That_Order_Service_Gets_All_Genres()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GenreRepository.GetAll()).Verifiable();

            var genreService = new GenreService(mock.Object);

            // Act
            genreService.GetAll();

            // Assert
            mock.Verify(m => m.GenreRepository.GetAll());
        }

        [TestMethod]
        public void Check_That_Order_Service_Checks_If_Genre_Exists()
        {
            // Arrange
            Genre genre = GetGenre();

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GenreRepository.Get(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns<Expression<Func<Genre, bool>>>(expr => new List<Genre> { genre });

            var genreService = new GenreService(mock.Object);

            const string genreName = "GenreName";
            const int currentGenreId = 1;

            // Act
            bool result = genreService.GenreExists(genreName, currentGenreId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Order_Service_Checks_If_Genre_Does_Not_Exists()
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GenreRepository.Get(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns<Expression<Func<Genre, bool>>>(expr => new List<Genre>());

            var genreService = new GenreService(mock.Object);

            const string genreName = "GenreName";
            const int currentGenreId = 1;

            // Act
            bool result = genreService.GenreExists(genreName, currentGenreId);

            // Assert
            Assert.IsTrue(!result);
        }
    }
}
