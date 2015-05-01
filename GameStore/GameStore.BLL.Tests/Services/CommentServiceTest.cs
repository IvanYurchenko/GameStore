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
    public class CommentServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        #region Positive tests

        [TestMethod]
        public void Check_That_Comment_Service_Adds_Comment()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()))
                .Returns(new Game());
            mock.Setup(m => m.CommentRepository.Insert(It.IsAny<Comment>()));

            var commentService = new CommentService(mock.Object);

            const string testKey = "key";

            //Act
            commentService.Add(new CommentModel(), testKey);

            //Assert
            mock.Verify(m => m.CommentRepository.Insert(It.IsAny<Comment>()));
            mock.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void Check_That_Comment_Service_Updates_Comment()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.CommentRepository.GetById(It.IsAny<int>()))
                .Returns(new Comment());
            mock.Setup(m => m.CommentRepository.Update(It.IsAny<Comment>()));

            var commentModel = new CommentModel {CommentId = 1};
            var commentService = new CommentService(mock.Object);

            //Act
            commentService.Update(commentModel);

            //Assert
            mock.Verify(m => m.CommentRepository.GetById(It.IsAny<int>()));
            mock.Verify(m => m.CommentRepository.Update(It.IsAny<Comment>()));
            mock.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void Check_That_Comment_Service_Removes_Comment()
        {
            //Arrange
            var comment = new Comment {IsRemoved = false};

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.CommentRepository.GetById(It.IsAny<int>()))
                .Returns(comment);
            mock.Setup(m => m.CommentRepository.Update(It.IsAny<Comment>()));

            var commentService = new CommentService(mock.Object);
            const int id = 1;

            //Act
            commentService.Remove(id);

            //Assert
            mock.Verify(m => m.CommentRepository.GetById(It.IsAny<int>()));
            Assert.IsTrue(comment.IsRemoved);
            mock.Verify(m => m.CommentRepository.Update(It.IsAny<Comment>()));
            mock.Verify(m => m.SaveChanges());
        }

        #endregion

        #region Exception tests

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Comment_Service_Add_Comment_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.GameRepository.GetGameByKey(It.IsAny<string>()))
                .Returns(new Game());
            mock.Setup(m => m.CommentRepository.Insert(It.IsAny<Comment>()))
                .Callback(() => { throw new Exception(); });

            var commentService = new CommentService(mock.Object);

            var commentModel = new CommentModel {CommentId = 1};
            const string key = "key";

            //Act
            commentService.Add(commentModel, key);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Comment_Service_Update_Comment_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.CommentRepository.GetById(It.IsAny<int>()))
                .Returns(new Comment());
            mock.Setup(m => m.CommentRepository.Update(It.IsAny<Comment>()))
                .Callback(() => { throw new Exception(); });

            var commentService = new CommentService(mock.Object);
            var commentModel = new CommentModel {CommentId = 1};

            //Act
            commentService.Update(commentModel);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void Check_That_Comment_Service_Remove_Comment_Rethrows_An_Exception()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.CommentRepository.GetById(It.IsAny<int>()))
                .Returns(new Comment());
            mock.Setup(m => m.CommentRepository.Update(It.IsAny<Comment>()))
                .Callback(() => { throw new Exception(); });

            var commentService = new CommentService(mock.Object);
            const int id = 1;

            //Act
            commentService.Remove(id);
        }

        #endregion
    }
}