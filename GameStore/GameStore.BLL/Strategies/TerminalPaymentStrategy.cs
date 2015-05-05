using System;
using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models.Payment;

namespace GameStore.BLL.Strategies
{
    public class TerminalPaymentStrategy : IPaymentStrategy
    {
        private const decimal DiscountForShooters = (decimal) 0.15;
        private const string DiscountedGenreName = "Shooter";

        public PaymentModel GetFinalPaymentModel(PaymentModel paymentModel)
        {
            foreach (var paymentItemModel
                in
                paymentModel.OrderItems.Where(
                    item => item.Game.Genres.Any(genre => String.Equals(genre.Name, DiscountedGenreName))))
            {
                paymentItemModel.Discount = paymentItemModel.Discount == 0
                    ? DiscountForShooters
                    : (1 - paymentItemModel.Discount)*DiscountForShooters + paymentItemModel.Discount;
            }

            return paymentModel;
        }
    }
}