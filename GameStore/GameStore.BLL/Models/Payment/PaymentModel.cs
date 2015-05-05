using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;

namespace GameStore.BLL.Models.Payment
{
    public class PaymentModel
    {
        public PaymentType PaymentType { get; set; }

        public PaymentInfoModel PaymentInfo { get; set; }

        public ICollection<OrderItemModel> OrderItems { get; set; }

        public decimal Sum
        {
            get
            {
                decimal sum = OrderItems.Sum(x => x.Price*(1 - x.Discount)*x.Quantity);
                return sum;
            }
        }
    }
}