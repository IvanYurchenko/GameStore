using System.Linq;
using AutoMapper;
using GameStore.DAL.Comparing;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Extentions;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind;

namespace GameStore.DAL.Synchronizing
{
    public class OrderItemSynchronizer
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        private readonly ICustomComparerFactory _comparerFactory;

        public OrderItemSynchronizer(GameStoreDbContext gameStoreDbContext, NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;

            _comparerFactory = new CustomComparerFactory();
        }

        public void Synchronize(Order_Detail orderDetail)
        {
            var gameStoreDbSet = _gameStoreDbContext.Set<OrderItem>();
            var northwindDbSet = _northwindDbContext.Set<Order_Detail>();

            if (orderDetail.IsProxy())
            {
                orderDetail = orderDetail.UnProxy(_northwindDbContext);
            }

            var northwindOrderItem = Mapper.Map<OrderItem>(orderDetail);

            var gameStoreOrderItem = gameStoreDbSet
                .FirstOrDefault(x => x.NorthwindOrderId == orderDetail.OrderID
                                     && x.NorthwindProductId == orderDetail.ProductID);

            if (gameStoreOrderItem == null)
            {
                _gameStoreDbContext.Set<OrderItem>().Add(northwindOrderItem);
            }
            else
            {
                if (gameStoreOrderItem.IsProxy())
                {
                    gameStoreOrderItem = gameStoreOrderItem.UnProxy(_gameStoreDbContext);
                }

                northwindOrderItem.OrderItemId = gameStoreOrderItem.OrderItemId;

                ICustomComparer comparer = _comparerFactory.GetComparer(typeof (OrderItem));

                if (!comparer.AreEqual(gameStoreOrderItem, northwindOrderItem))
                {
                    _gameStoreDbContext.Entry(gameStoreOrderItem).CurrentValues.SetValues(northwindOrderItem);
                }
            }
        }
    }
}