using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Localization;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Mappings;
using GameStore.WebUI.ViewModels;
using GameStore.WebUI.ViewModels.Localization;
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

        private static PublisherController GetPublisherController(
            IMock<IPublisherService> mockPublisherService = null,
            IMock<ILanguageService> mockLanguageService = null 
            )
        {
            mockPublisherService = mockPublisherService ?? new Mock<IPublisherService>();
            mockLanguageService = mockLanguageService ?? new Mock<ILanguageService>();

            var publisherController = new PublisherController(
                mockPublisherService.Object,
                mockLanguageService.Object);

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

        private static PublisherAddUpdateViewModel GetPublisherAddUpdateViewModel()
        {
            var publisherAddUpdateViewModel = new PublisherAddUpdateViewModel
            {
                IsReadonly = false,
                NorthwindId = 5,
                PublisherId = 5,
                HomePage = "http://www.homepage.com",
                PublisherLocalizations = new List<PublisherLocalizationViewModel>
                {
                    new PublisherLocalizationViewModel
                    {
                        CompanyName = "name",
                        Description = "descr",
                        Language = new LanguageViewModel
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        },
                        LanguageId = 1,
                    }
                }
            };

            return publisherAddUpdateViewModel;
        }

        private static PublisherModel GetPublisherModel()
        {
            var publisherModel = new PublisherModel
            {
                PublisherLocalizations = new List<PublisherLocalizationModel>
                {
                    new PublisherLocalizationModel
                    {
                        CompanyName = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new LanguageModel
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
            };

            return publisherModel;
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Add_Action()
        {
            // Arrange
            var mockPublisherService = new Mock<IPublisherService>();
            mockPublisherService.Setup(x => x.Add(It.IsAny<PublisherModel>())).Verifiable();

            PublisherController publisherController = GetPublisherController(mockPublisherService);

            PublisherAddUpdateViewModel publisherAddUpdateViewModel = GetPublisherAddUpdateViewModel();

            // Act
            publisherController.Add(publisherAddUpdateViewModel);

            // Assert
            mockPublisherService.Verify(m => m.Add(It.IsAny<PublisherModel>()));
        }

        [TestMethod]
        public void Check_That_Right_Method_Was_Called_Inside_Details_Action()
        {
            // Arrange
            var mockPublisherService = new Mock<IPublisherService>();
            mockPublisherService.Setup(x => x.GetModelByCompanyName(It.IsAny<string>()))
                .Returns(GetPublisherModel);

            PublisherController publisherController = GetPublisherController(mockPublisherService);

            const string key = "key";

            // Act
            publisherController.GetDetails(key);

            // Assert
            mockPublisherService.Verify(m => m.GetModelByCompanyName(It.IsAny<string>()));
        }
    }
}
