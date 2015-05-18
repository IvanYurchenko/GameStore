using System;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing.Concrete
{
    public class OrderItemComparer : ICustomComparer
    {
        public bool AreEqual(Object object1, Object object2)
        {
            if (object1.GetType() != typeof (OrderItem) || object2.GetType() != typeof (OrderItem))
            {
                throw new ArgumentException(String.Format("An object is not an instance of a {0} class. ",
                    typeof (OrderItem)));
            }

            var orderItem1 = (OrderItem) object1;
            var orderItem2 = (OrderItem) object2;

            var result = orderItem1.Price == orderItem2.Price
                         && orderItem1.Quantity == orderItem2.Quantity
                         && orderItem1.Discount == orderItem2.Discount
                         && orderItem1.GameId == orderItem2.GameId
                         && orderItem1.OrderId == orderItem2.OrderId
                         && orderItem1.IsReadonly == orderItem2.IsReadonly;

            return result;
        }
    }
}