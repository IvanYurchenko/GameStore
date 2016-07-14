using GameStore.BLL.Enums;
using GameStore.BLL.Models.Payment;

namespace GameStore.BLL.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Gets the payment model.
        /// </summary>
        /// <param name="sessionKey">The session key.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <param name="paymentInfoModel">Payment information.</param>
        /// <returns></returns>
        PaymentModel CreatePaymentModel(string sessionKey, PaymentType paymentType, PaymentInfoModel paymentInfoModel);
    }
}