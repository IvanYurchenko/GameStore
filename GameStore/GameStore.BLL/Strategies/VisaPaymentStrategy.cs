using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Payment;

namespace GameStore.BLL.Strategies
{
    public class VisaPaymentStrategy : IPaymentStrategy
    {
        private const decimal DiscountForAllGames = (decimal) 0.05;

        public PaymentModel GetFinalPaymentModel(PaymentModel paymentModel)
        {
            foreach (OrderItemModel paymentItemModel in paymentModel.OrderItems)
            {
                paymentItemModel.Discount = paymentItemModel.Discount == 0
                    ? DiscountForAllGames
                    : (1 - paymentItemModel.Discount)*DiscountForAllGames + paymentItemModel.Discount;
            }

            return paymentModel;
        }
    }
}