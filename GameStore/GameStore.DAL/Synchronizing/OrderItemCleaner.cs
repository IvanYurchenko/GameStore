using System.Data.Entity;
using System.Linq;
using GameStore.DAL.Comparing;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind;

namespace GameStore.DAL.Synchronizing
{
    public class OrderItemCleaner
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        private readonly ICustomComparerFactory _comparerFactory;

        public OrderItemCleaner(GameStoreDbContext gameStoreDbContext, NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;

            _comparerFactory = new CustomComparerFactory();
        }

        public void DeleteUnusedEntities()
        {
            DbSet<OrderItem> gameStoreDbSet = _gameStoreDbContext.Set<OrderItem>();
            DbSet<Order_Detail> northwindDbSet = _northwindDbContext.Set<Order_Detail>();

            foreach (
                OrderItem orderItem in gameStoreDbSet.Where(x => x.NorthwindOrderId != null && x.NorthwindProductId != null))
            {
                if (!northwindDbSet.Any(od => od.OrderID == orderItem.NorthwindOrderId
                                              && od.ProductID == orderItem.NorthwindOrderId))
                {
                    gameStoreDbSet.Remove(orderItem);
                }
            }
        }
    }
}