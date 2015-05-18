using System.Collections.Generic;
using System.Data;
using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Northwind;

namespace GameStore.DAL.Synchronizing.NavigationProperties
{
    public class PublisherNavigationPropertiesSynchronizer
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        public PublisherNavigationPropertiesSynchronizer(GameStoreDbContext gameStoreDbContext,
            NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;
        }

        public void SynchronizeNavigationProperties(Publisher publisher)
        {
            Supplier supplier = _northwindDbContext.Suppliers.First(x => x.SupplierID == publisher.NorthwindId);

            publisher.Games = new List<Game>();

            foreach (var product in supplier.Products)
            {
                var game = _gameStoreDbContext.Set<Game>().FirstOrDefault(g => g.NorthwindId == product.ProductID);
                if (game != null)
                {
                    publisher.Games.Add(game);
                }
            }

            if (_gameStoreDbContext.Entry(publisher).State == EntityState.Detached)
            {
                _gameStoreDbContext.Set<Publisher>().Attach(publisher);
            }

            _gameStoreDbContext.Entry(publisher).State = EntityState.Modified;
        }
    }
}