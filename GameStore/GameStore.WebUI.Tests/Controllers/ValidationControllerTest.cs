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
            IMock<IPublisherService> mockPublisherService = null)
        {
            mockGameService = mockGameService ?? new Mock<IGameService>();
            mockPublisherService = mockPublisherService ?? new Mock<IPublisherService>();

            var validationController = new ValidationController(
                mockGameService.Object,
                mockPublisherService.Object);

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

            var validationController = GetValidationController(mockGameService);

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

            var validationController = GetValidationController(mockPublisherService: mockPublisherService);

            const string companyName = "company name";
            const int currentPublisherId = 1;

            // Act
            validationController.IsCompanyNameAvailable(companyName, currentPublisherId);

            // Assert
            mockPublisherService.Verify(m => m.PublisherExists(companyName, currentPublisherId));
        }
    }
}