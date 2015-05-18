using System;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing.Concrete
{
    public class OrderComparer : ICustomComparer
    {
        public bool AreEqual(Object object1, Object object2)
        {
            if (object1.GetType() != typeof (Order) || object2.GetType() != typeof (Order))
            {
                throw new ArgumentException(String.Format("An object is not an instance of a {0} class. ",
                    typeof (Order)));
            }

            var order1 = (Order) object1;
            var order2 = (Order) object2;

            var orderItemsEquals = true;

            if (order1.OrderItems != null && order2.OrderItems != null)
            {
                foreach (var orderItem in order1.OrderItems)
                {
                    if (!order2.OrderItems.Any(oi => oi.GameId != orderItem.GameId))
                    {
                        orderItemsEquals = false;
                    }
                }
            }

            if ((order1.OrderItems == null && order2.OrderItems != null && order2.OrderItems.Count > 0)
                || (order1.OrderItems != null && order2.OrderItems == null && order1.OrderItems.Count > 0))
            {
                orderItemsEquals = false;
            }

            var result = order1.IsPayed == order2.IsPayed
                         && order1.OrderDate == order2.OrderDate
                         && order1.NorthwindId == order2.NorthwindId
                         && order1.IsReadonly == order2.IsReadonly
                         && orderItemsEquals;

            return result;
        }
    }
}