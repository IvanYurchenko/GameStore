using System.Collections.Generic;
using System.Data;
using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Northwind;
using Order = GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Synchronizing.NavigationProperties
{
    public class OrderNavigationPropertiesSynchronizer
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        public OrderNavigationPropertiesSynchronizer(GameStoreDbContext gameStoreDbContext,
            NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;
        }

        public void SynchronizeNavigationProperties(Order order)
        {
            Northwind.Order northwindOrder = _northwindDbContext.Orders.First(x => x.OrderID == order.NorthwindId);

            order.OrderItems = new List<OrderItem>();

            foreach (Order_Detail orderDetail in northwindOrder.Order_Details)
            {
                OrderItem orderItem = _gameStoreDbContext.Set<OrderItem>()
                    .FirstOrDefault(oi => oi.NorthwindOrderId == orderDetail.OrderID
                                          && oi.NorthwindProductId == orderDetail.ProductID);

                if (orderItem != null)
                {
                    order.OrderItems.Add(orderItem);
                }
            }

            if (_gameStoreDbContext.Entry(order).State == EntityState.Detached)
            {
                _gameStoreDbContext.Set<Order>().Attach(order);
            }

            _gameStoreDbContext.Entry(order).State = EntityState.Modified;
        }
    }
}