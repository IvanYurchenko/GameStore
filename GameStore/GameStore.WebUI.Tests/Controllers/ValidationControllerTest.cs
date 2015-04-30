using GameStore.BLL.Interfaces;
using GameStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.WebUI.Tests.Controllers
{
    [TestClass]
    public class ValidationControllerTest
    {
        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_IsGameKeyAvailable_Action()
        {
            // Arrange
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(m => m.GameExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);
            var mockPublisherService = new Mock<IPublisherService>();

            var validationController = new ValidationController(mockGameService.Object, mockPublisherService.Object);

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
            var mockGameService = new Mock<IGameService>();
            var mockPublisherService = new Mock<IPublisherService>();
            mockPublisherService.Setup(m => m.PublisherExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);

            var validationController = new ValidationController(mockGameService.Object, mockPublisherService.Object);

            const string companyName = "company name";
            const int currentPublisherId = 1;

            // Act
            validationController.IsCompanyNameAvailable(companyName, currentPublisherId);

            // Assert
            mockPublisherService.Verify(m => m.PublisherExists(companyName, currentPublisherId));
        }
    }
}