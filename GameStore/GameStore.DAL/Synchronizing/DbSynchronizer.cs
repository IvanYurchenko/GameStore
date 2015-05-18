using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind;
using GameStore.DAL.Synchronizing.NavigationProperties;
using Order = GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Synchronizing
{
    public class DbSynchronizer : IDbSynchronizer
    {
        #region Contexts

        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        #endregion

        #region Syncrhonizers

        private readonly GenericSynchronizer<Game, Product> _gameProductSynchronizer;
        private readonly GenericSynchronizer<Genre, Category> _genreCategorySynchronizer;
        private readonly GenericSynchronizer<Publisher, Supplier> _publisherSupplierSynchronizer;
        private readonly GenericSynchronizer<Order, Northwind.Order> _orderNorthwindOrderSynchronizer;
        private readonly OrderItemSynchronizer _orderItemOrderDetailSynchronizer;
        
        #endregion

        #region Cleaners

        private readonly GenericCleaner<Game, Product> _gameProductCleaner;
        private readonly GenericCleaner<Genre, Category> _genreCategoryCleaner;
        private readonly GenericCleaner<Publisher, Supplier> _publisherSupplierCleaner;
        private readonly GenericCleaner<Order, Northwind.Order> _orderNorthwindOrderCleaner;
        private readonly OrderItemCleaner _orderItemOrderDetailCleaner;

        #endregion

        #region Navigation Properties Synchronizers

        private readonly GameNavigationPropertiesSynchronizer _gameNavigationPropertiesSynchronizer;
        private readonly GenreNavigationPropertiesSynchronizer _genreNavigationPropertiesSynchronizer;
        private readonly OrderNavigationPropertiesSynchronizer _orderNavigationPropertiesSynchronizer;
        private readonly OrderItemNavigationPropertiesSynchronizer _orderItemNavigationPropertiesSynchronizer;
        private readonly PublisherNavigationPropertiesSynchronizer _publisherNavigationPropertiesSynchronizer;

        #endregion 

        public DbSynchronizer(GameStoreDbContext gameStoreDbContext, NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;

            _gameProductSynchronizer =
                new GenericSynchronizer<Game, Product>(_gameStoreDbContext, _northwindDbContext);
            _genreCategorySynchronizer =
                new GenericSynchronizer<Genre, Category>(_gameStoreDbContext, _northwindDbContext);
            _publisherSupplierSynchronizer =
                new GenericSynchronizer<Publisher, Supplier>(_gameStoreDbContext, _northwindDbContext);
            _orderNorthwindOrderSynchronizer =
                new GenericSynchronizer<Order, Northwind.Order>(_gameStoreDbContext, _northwindDbContext);
            _orderItemOrderDetailSynchronizer =
                new OrderItemSynchronizer(_gameStoreDbContext, _northwindDbContext);

            _gameProductCleaner =
                new GenericCleaner<Game, Product>(_gameStoreDbContext, _northwindDbContext);
            _genreCategoryCleaner =
                new GenericCleaner<Genre, Category>(_gameStoreDbContext, _northwindDbContext);
            _publisherSupplierCleaner =
                new GenericCleaner<Publisher, Supplier>(_gameStoreDbContext, _northwindDbContext);
            _orderNorthwindOrderCleaner =
                new GenericCleaner<Order, Northwind.Order>(_gameStoreDbContext, _northwindDbContext);
            _orderItemOrderDetailCleaner =
                new OrderItemCleaner(_gameStoreDbContext, _northwindDbContext);

            _gameNavigationPropertiesSynchronizer =
                new GameNavigationPropertiesSynchronizer(_gameStoreDbContext, _northwindDbContext);
            _genreNavigationPropertiesSynchronizer =
                new GenreNavigationPropertiesSynchronizer(_gameStoreDbContext, _northwindDbContext);
            _orderNavigationPropertiesSynchronizer =
                new OrderNavigationPropertiesSynchronizer(_gameStoreDbContext, _northwindDbContext);
            _orderItemNavigationPropertiesSynchronizer =
                new OrderItemNavigationPropertiesSynchronizer(_gameStoreDbContext, _northwindDbContext);
            _publisherNavigationPropertiesSynchronizer =
                new PublisherNavigationPropertiesSynchronizer(_gameStoreDbContext, _northwindDbContext);
        }

        public void SynchronizeDatabases()
        {
            #region Synchronize without navigation properties

            foreach (var product in _northwindDbContext.Set<Product>())
            {
                _gameProductSynchronizer.Synchronize(product);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (var category in _northwindDbContext.Set<Category>())
            {
                _genreCategorySynchronizer.Synchronize(category);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (var supplier in _northwindDbContext.Set<Supplier>())
            {
                _publisherSupplierSynchronizer.Synchronize(supplier);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (var northwindOrder in _northwindDbContext.Set<Northwind.Order>())
            {
                _orderNorthwindOrderSynchronizer.Synchronize(northwindOrder);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (var orderDetail in _northwindDbContext.Set<Order_Detail>())
            {
                _orderItemOrderDetailSynchronizer.Synchronize(orderDetail);
            }

            _gameStoreDbContext.SaveChanges();

            #endregion

            #region Synchronizing deleted entities

            _gameProductCleaner.DeleteUnusedEntities();
            _genreCategoryCleaner.DeleteUnusedEntities();
            _publisherSupplierCleaner.DeleteUnusedEntities();
            _orderNorthwindOrderCleaner.DeleteUnusedEntities();
            _orderItemOrderDetailCleaner.DeleteUnusedEntities();

            _gameStoreDbContext.SaveChanges();

            #endregion

            #region Synchronizing navigation properties

            foreach (var game in _gameStoreDbContext.Set<Game>().Where(x => x.NorthwindId != null).ToList())
            {
                _gameNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(game);
            }

            foreach (var publisher in _gameStoreDbContext.Set<Publisher>().Where(x => x.NorthwindId != null).ToList())
            {
                _publisherNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(publisher);
            }

            foreach (var genre in _gameStoreDbContext.Set<Genre>().Where(x => x.NorthwindId != null).ToList())
            {
                _genreNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(genre);
            }

            foreach (var order in _gameStoreDbContext.Set<Order>().Where(x => x.NorthwindId != null).ToList())
            {
                _orderNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(order);
            }

            foreach (var orderItem in _gameStoreDbContext.Set<OrderItem>()
                .Where(x => x.NorthwindOrderId != null && x.NorthwindProductId != null).ToList())
            {
                _orderItemNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(orderItem);
            }

            _gameStoreDbContext.SaveChanges();

            #endregion
        }
    }
}