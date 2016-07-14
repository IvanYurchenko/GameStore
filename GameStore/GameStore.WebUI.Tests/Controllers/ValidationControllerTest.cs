using GameStore.BLL.Interfaces;
using GameStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.WebUI.Tests.Controllers
{
    [TestClass]
    public class ValidationControllerTest
    {
        #region Helpers

        private static ValidationController GetValidationController(
            IMock<IGameService> mockGameService = null,
            IMock<IPublisherService> mockPublisherService = null,
            IMock<IUserService> mockUserService = null,
            IMock<IRoleService> mockRoleService = null,
            IMock<IGenreService> mockGenreService = null)
        {
            mockGameService = mockGameService ?? new Mock<IGameService>();
            mockPublisherService = mockPublisherService ?? new Mock<IPublisherService>();
            mockUserService = mockUserService ?? new Mock<IUserService>();
            mockRoleService = mockRoleService ?? new Mock<IRoleService>();
            mockGenreService = mockGenreService ?? new Mock<IGenreService>();

            var validationController = new ValidationController(
                mockGameService.Object,
                mockPublisherService.Object,
                mockUserService.Object,
                mockRoleService.Object,
                mockGenreService.Object);

            return validationController;
        }

        #endregion

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_IsGameKeyAvailable_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GameExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);

            ValidationController validationController = GetValidationController(mockGameService);

            const string key = "key";
            const int currentGameId = 1;

            // Act
            validationController.IsGameKeyAvailable(key, currentGameId);

            // Assert
            mockGameService.Verify(m => m.GameExists(key, currentGameId));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_IsCompanyNameAvailable_Action()
        {
            // Arrange
            var mockPublisherService = new Mock<IPublisherService>();
            mockPublisherService.Setup(m => m.PublisherExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);

            ValidationController validationController = GetValidationController(mockPublisherService: mockPublisherService);

            const string companyName = "company name";
            const int currentPublisherId = 1;

            // Act
            validationController.IsCompanyNameAvailable(companyName, currentPublisherId);

            // Assert
            mockPublisherService.Verify(m => m.PublisherExists(companyName, currentPublisherId));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_IsGenreNameAvailable_Action()
        {
            // Arrange
            var mockGenreService = new Mock<IGenreService>();
            mockGenreService.Setup(m => m.GenreExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);

            ValidationController validationController = GetValidationController(mockGenreService: mockGenreService);

            const string genreName = "name";
            const int currentGenreId = 1;

            // Act
            validationController.IsGenreNameAvailable(genreName, currentGenreId);

            // Assert
            mockGenreService.Verify(m => m.GenreExists(genreName, currentGenreId));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_IsUserNameAvailable_Action()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(m => m.UserExists(It.IsAny<string>()))
                .Returns(false);

            ValidationController validationController = GetValidationController(mockUserService: mockUserService);

            const string userName = "name";

            // Act
            validationController.IsUserNameAvailable(userName);

            // Assert
            mockUserService.Verify(m => m.UserExists(userName));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_IsRoleNameAvailable_Action()
        {
            // Arrange
            var mockRoleService = new Mock<IRoleService>();
            mockRoleService.Setup(m => m.RoleExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);

            ValidationController validationController = GetValidationController(mockRoleService: mockRoleService);

            const string roleName = "name";
            const int currentRoleId = 1;

            // Act
            validationController.IsRoleNameAvailable(roleName, currentRoleId);

            // Assert
            mockRoleService.Verify(m => m.RoleExists(roleName, currentRoleId));
        }
    }
}