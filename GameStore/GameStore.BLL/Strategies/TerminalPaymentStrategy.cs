using System;
using System.Linq;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Payment;
using GameStore.Core;

namespace GameStore.BLL.Strategies
{
    public class TerminalPaymentStrategy : IPaymentStrategy
    {
        private const decimal DiscountForShooters = (decimal) 0.15;
        private const string DiscountedGenreName = "Shooter";

        public PaymentModel GetFinalPaymentModel(PaymentModel paymentModel)
        {
            foreach (OrderItemModel paymentItemModel
                in
                paymentModel.OrderItems.Where(
                    item => item.Game.Genres.Any(genre =>
                        String.Equals(genre.GenreLocalizations.First(loc =>
                            String.Equals(loc.Language.Code, Constants.EnglishLanguageCode, StringComparison.CurrentCultureIgnoreCase))
                            .Name,
                            DiscountedGenreName, 
                            StringComparison.CurrentCultureIgnoreCase))))
            {
                paymentItemModel.Discount = paymentItemModel.Discount == 0
                    ? DiscountForShooters
                    : (1 - paymentItemModel.Discount)*DiscountForShooters + paymentItemModel.Discount;
            }

            return paymentModel;
        }
    }
}