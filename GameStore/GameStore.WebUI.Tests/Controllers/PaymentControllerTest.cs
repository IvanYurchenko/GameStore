using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.WebUI.Tests.Controllers
{
    [TestClass]
    public class PaymentControllerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        #region Helpers

        private static PaymentController GetPaymentController(
            Mock<IPaymentService> mockPaymentService = null,
            Mock<IOrderService> mockOrderService = null)
        {
            mockPaymentService = mockPaymentService ?? new Mock<IPaymentService>();
            mockOrderService = mockOrderService ?? new Mock<IOrderService>();

            var paymentController = new PaymentController(mockPaymentService.Object, mockOrderService.Object);
            
            return paymentController;
        }

        private static string GetKey()
        {
            return "TestKey";
        }

        private static string GetSessionKey()
        {
            return "SessionKey";
        }

        #endregion
    }
}