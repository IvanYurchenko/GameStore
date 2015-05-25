using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.WebUI.Tests.Controllers
{
    [TestClass]
    public class CommentControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        #region Helpers

        private static CommentController GetCommentController(
            Mock<IGameService> mockGameService = null,
            Mock<ICommentService> mockCommentService = null)
        {
            mockGameService = mockGameService ?? new Mock<IGameService>();
            mockCommentService = mockCommentService ?? new Mock<ICommentService>();

            var commentController = new CommentController(mockGameService.Object, mockCommentService.Object);

            return commentController;
        }

        private static string GetKey()
        {
            return "TestKey";
        }

        private static CommentModel GetCommentModel()
        {
            var commentModel = new CommentModel
            {
                GameId = 5,
                Name = "CommentName",
                Body = "Body",
            };

            return commentModel;
        }

        private static GameModel GetGameModel()
        {
            var gameModel = new GameModel
            {
                GameId = 2,
                Key = "testKey",
                Comments = new List<CommentModel>
                {
                    new CommentModel {CommentId = 1, Body = "Test Body", GameId = 2, Name = "Test Name"}
                }
            };

            return gameModel;
        }

        #endregion

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_NewComment_Action()
        {
            // Arrange
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(m => m.Add(It.IsAny<CommentModel>(), It.IsAny<string>())).Verifiable();

            CommentController commentController = GetCommentController(mockCommentService: mockCommentService);

            string key = GetKey();
            CommentModel commentModel = GetCommentModel();

            // Act
            commentController.AddCommentForGame(key, commentModel);

            // Assert
            mockCommentService.Verify(m => m.Add(commentModel, key));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Comments_Action()
        {
            // Arrange
            GameModel gameModel = GetGameModel();

            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GetGameModelByKey(It.IsAny<string>()))
                .Returns<string>(p => gameModel);

            CommentController commentController = GetCommentController(mockGameService);

            string key = GetKey();

            // Act
            commentController.GetCommentsForGame(key);

            // Assert
            mockGameService.Verify(m => m.GetGameModelByKey(key));
        }
    }
}