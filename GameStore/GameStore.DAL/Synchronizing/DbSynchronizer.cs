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
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;
        
        private GenericSynchronizer<Game, Product> _gameProductSynchronizer;
        private GenericSynchronizer<Genre, Category> _genreCategorySynchronizer;
        private GenericSynchronizer<Publisher, Supplier> _publisherSupplierSynchronizer;
        private GenericSynchronizer<Order, Northwind.Order> _orderNorthwindOrderSynchronizer;
        private OrderItemSynchronizer _orderItemOrderDetailSynchronizer;
        
        private GenericCleaner<Game, Product> _gameProductCleaner;
        private GenericCleaner<Genre, Category> _genreCategoryCleaner;
        private GenericCleaner<Publisher, Supplier> _publisherSupplierCleaner;
        private GenericCleaner<Order, Northwind.Order> _orderNorthwindOrderCleaner;
        private OrderItemCleaner _orderItemOrderDetailCleaner;
        
        private GameNavigationPropertiesSynchronizer _gameNavigationPropertiesSynchronizer;
        private GenreNavigationPropertiesSynchronizer _genreNavigationPropertiesSynchronizer;
        private OrderNavigationPropertiesSynchronizer _orderNavigationPropertiesSynchronizer;
        private OrderItemNavigationPropertiesSynchronizer _orderItemNavigationPropertiesSynchronizer;
        private PublisherNavigationPropertiesSynchronizer _publisherNavigationPropertiesSynchronizer;
        
        public DbSynchronizer(GameStoreDbContext gameStoreDbContext, NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;

            InitEntitiesSynchronizers();
            InitCleaners();
            InitNavigationPropertiesSynchronizers();
        }

        public void SynchronizeDatabases()
        {
            SynchronizeEntities();
            CleanRemovedEntities();
            SynchronizeNavigationProperties();
        }

        private void SynchronizeEntities()
        {
            foreach (Product product in _northwindDbContext.Set<Product>())
            {
                _gameProductSynchronizer.Synchronize(product);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (Category category in _northwindDbContext.Set<Category>())
            {
                _genreCategorySynchronizer.Synchronize(category);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (Supplier supplier in _northwindDbContext.Set<Supplier>())
            {
                _publisherSupplierSynchronizer.Synchronize(supplier);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (Northwind.Order northwindOrder in _northwindDbContext.Set<Northwind.Order>())
            {
                _orderNorthwindOrderSynchronizer.Synchronize(northwindOrder);
            }

            _gameStoreDbContext.SaveChanges();

            foreach (Order_Detail orderDetail in _northwindDbContext.Set<Order_Detail>())
            {
                _orderItemOrderDetailSynchronizer.Synchronize(orderDetail);
            }

            _gameStoreDbContext.SaveChanges();

        }

        private void CleanRemovedEntities()
        {
            _gameProductCleaner.DeleteUnusedEntities();
            _genreCategoryCleaner.DeleteUnusedEntities();
            _publisherSupplierCleaner.DeleteUnusedEntities();
            _orderNorthwindOrderCleaner.DeleteUnusedEntities();
            _orderItemOrderDetailCleaner.DeleteUnusedEntities();

            _gameStoreDbContext.SaveChanges();
        }

        private void SynchronizeNavigationProperties()
        {
            foreach (Game game in _gameStoreDbContext.Set<Game>().Where(x => x.NorthwindId != null).ToList())
            {
                _gameNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(game);
            }

            foreach (Publisher publisher in _gameStoreDbContext.Set<Publisher>().Where(x => x.NorthwindId != null).ToList())
            {
                _publisherNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(publisher);
            }

            foreach (Genre genre in _gameStoreDbContext.Set<Genre>().Where(x => x.NorthwindId != null).ToList())
            {
                _genreNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(genre);
            }

            foreach (Order order in _gameStoreDbContext.Set<Order>().Where(x => x.NorthwindId != null).ToList())
            {
                _orderNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(order);
            }

            foreach (OrderItem orderItem in _gameStoreDbContext.Set<OrderItem>()
                .Where(x => x.NorthwindOrderId != null && x.NorthwindProductId != null).ToList())
            {
                _orderItemNavigationPropertiesSynchronizer.SynchronizeNavigationProperties(orderItem);
            }

            _gameStoreDbContext.SaveChanges();
        }

        private void InitEntitiesSynchronizers()
        {
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
        }

        private void InitCleaners()
        {
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
        }

        private void InitNavigationPropertiesSynchronizers()
        {
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
    }
}