using GameStore.BLL.Models.Payment;

namespace GameStore.BLL.Interfaces
{
    public interface IPaymentStrategy
    {
        PaymentModel GetFinalPaymentModel(PaymentModel paymentModel);
    }
}