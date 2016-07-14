using GameStore.BLL.Models.Payment;

namespace GameStore.BLL.Interfaces
{
    public interface IPaymentStrategy
    {
        /// <summary>
        /// Gets the final payment model after applying needed modifications to it (discounts etc.).
        /// </summary>
        /// <param name="paymentModel">The payment model.</param>
        /// <returns></returns>
        PaymentModel GetFinalPaymentModel(PaymentModel paymentModel);
    }
}