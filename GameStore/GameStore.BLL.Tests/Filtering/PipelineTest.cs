using AutoMapper;
using GameStore.BLL.Filtering;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.BLL.Tests.Filtering
{
    [TestClass]
    public class PipelineTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Check_That_Pipeline_Registers_Filter()
        {
            //Arrange
            var pipeline = new Pipeline<GameFilterContainer>();
            var mockFilter = new Mock<IFilter<GameFilterContainer>>();

            //Act
            pipeline.BeginRegister(mockFilter.Object);

            //Assert
            Assert.IsTrue(pipeline.Filter != null);
        }

        [TestMethod]
        public void Check_That_Pipeline_Executes_Filter()
        {
            //Arrange
            var pipeline = new Pipeline<GameFilterContainer>();
            var container = new GameFilterContainer();
            var mockFilter = new Mock<IFilter<GameFilterContainer>>();
            mockFilter.Setup(m => m.Execute(It.IsAny<GameFilterContainer>()));
            pipeline.Filter = mockFilter.Object;

            //Act
            pipeline.ExecuteAll(container);

            //Assert
            mockFilter.Verify(m => m.Execute(It.IsAny<GameFilterContainer>()));
        }
    }
}