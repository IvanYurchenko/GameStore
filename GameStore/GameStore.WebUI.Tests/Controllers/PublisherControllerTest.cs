using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Mappings;
using GameStore.WebUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.WebUI.Tests.Controllers
{
    [TestClass]
    public class PublisherControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        #region Helpers

        private static PublisherController GetPublisherController(
            IMock<IPublisherService> mockPublisherService = null
            )
        {
            mockPublisherService = mockPublisherService ?? new Mock<IPublisherService>();

            var publisherController = new PublisherController(mockPublisherService.Object);

            return publisherController;
        }

        private static PublisherViewModel GetPublisherViewModel()
        {
            var publisherViewModel = new PublisherViewModel
            {
                IsReadonly = false,
                NorthwindId = 5,
                Description = "description",
                PublisherId = 5,
                CompanyName = "name",
                HomePage = "http://www.homepage.com"
            };

            return publisherViewModel;
        }

        #endregion

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Add_Action()
        {
            // Arrange
            var mockPublisherService = new Mock<IPublisherService>();
            mockPublisherService.Setup(x => x.Add(It.IsAny<PublisherModel>())).Verifiable();

            var publisherController = GetPublisherController(mockPublisherService);

            PublisherViewModel publisherViewModel = GetPublisherViewModel();

            // Act
            publisherController.Add(publisherViewModel);

            // Assert
            mockPublisherService.Verify(m => m.Add(It.IsAny<PublisherModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Details_Action()
        {
            // Arrange
            var mockPublisherService = new Mock<IPublisherService>();
            mockPublisherService.Setup(x => x.GetModelByCompanyName(It.IsAny<string>()))
                .Returns(new PublisherModel());

            var publisherController = GetPublisherController(mockPublisherService);

            var key = "key";

            // Act
            publisherController.GetDetails(key);

            // Assert
            mockPublisherService.Verify(m => m.GetModelByCompanyName(It.IsAny<string>()));
        }
    }
}
