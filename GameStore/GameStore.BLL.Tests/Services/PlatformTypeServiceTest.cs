using System.Collections.Generic;
using System.Linq;
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
    public class PlatformTypeServiceTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Check_That_Platform_Type_Service_Gets_All_Platform_Types()
        {
            //Arrange
            var testList = new List<PlatformType>
            {
                new PlatformType(),
                new PlatformType(),
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.PlatformTypeRepository.GetAll())
                .Returns(testList);

            var platformTypeService = new PlatformTypeService(mock.Object);

            //Act
            IEnumerable<PlatformTypeModel> platformTypes = platformTypeService.GetAll();

            //Assert
            Assert.IsTrue(testList.Count == platformTypes.Count());
        }
    }
}