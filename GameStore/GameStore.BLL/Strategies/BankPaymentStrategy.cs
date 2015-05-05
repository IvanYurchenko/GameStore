using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Payment;

namespace GameStore.BLL.Strategies
{
    public class BankPaymentStrategy : IPaymentStrategy
    {
        public PaymentModel GetFinalPaymentModel(PaymentModel paymentModel)
        {
            return paymentModel;
        }
    }
}