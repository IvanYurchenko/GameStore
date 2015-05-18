using System.Data;
using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Northwind;
using Order = GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Synchronizing.NavigationProperties
{
    public class OrderItemNavigationPropertiesSynchronizer
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        public OrderItemNavigationPropertiesSynchronizer(GameStoreDbContext gameStoreDbContext,
            NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;
        }

        public void SynchronizeNavigationProperties(OrderItem orderItem)
        {
            orderItem.Order = _gameStoreDbContext.Set<Order>()
                .First(o => o.NorthwindId == orderItem.NorthwindOrderId);

            orderItem.OrderId = orderItem.Order.OrderId;

            orderItem.Game = _gameStoreDbContext.Set<Game>()
                .First(g => g.NorthwindId == orderItem.NorthwindProductId);

            orderItem.GameId = orderItem.Game.GameId;
            
            if (_gameStoreDbContext.Entry(orderItem).State == EntityState.Detached)
            {
                _gameStoreDbContext.Set<OrderItem>().Attach(orderItem);
            }

            _gameStoreDbContext.Entry(orderItem).State = EntityState.Modified;
        }
    }
}