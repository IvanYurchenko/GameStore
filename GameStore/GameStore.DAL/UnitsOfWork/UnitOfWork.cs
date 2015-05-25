//#define DBSYNC

using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Security;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind;
using GameStore.DAL.Repositories;
using Order = GameStore.DAL.Entities.Order;

namespace GameStore.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        private readonly IDbSynchronizer _dbSynchronizer;
        
        private IGameRepository _gameRepository;
        private IGenericRepository<Genre> _genreRepository;
        private IGenericRepository<Comment> _commentRepository;
        private IGenericRepository<PlatformType> _platformTypeRepository;
        private IGenericRepository<Basket> _basketRepository;
        private IGenericRepository<BasketItem> _basketItemRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<OrderItem> _orderItemRepository;
        private IGenericRepository<Publisher> _publisherRepository;
        private IGenericRepository<Shipper> _shipperRepository;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Role> _roleRepository;

        public UnitOfWork()
        {
            _gameStoreDbContext = new GameStoreDbContext();
            _northwindDbContext = new NorthwindDbContext();

#if DBSYNC
            if (!IsSynchronized)
            {
                _dbSynchronizer = new DbSynchronizer(_gameStoreDbContext, _northwindDbContext);
                _dbSynchronizer.SynchronizeDatabases();
                IsSynchronized = true;
            }
#endif
        }

        public static bool IsSynchronized { get; set; }

        public IGameRepository GameRepository
        {
            get { return _gameRepository ?? (_gameRepository = new GameRepository(_gameStoreDbContext)); }
        }

        public IGenericRepository<Genre> GenreRepository
        {
            get { return _genreRepository ?? (_genreRepository = new GenericRepository<Genre>(_gameStoreDbContext)); }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get
            {
                return _commentRepository ?? (_commentRepository = new GenericRepository<Comment>(_gameStoreDbContext));
            }
        }

        public IGenericRepository<PlatformType> PlatformTypeRepository
        {
            get
            {
                return _platformTypeRepository ??
                       (_platformTypeRepository = new GenericRepository<PlatformType>(_gameStoreDbContext));
            }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_gameStoreDbContext)); }
        }

        public IGenericRepository<OrderItem> OrderItemRepository
        {
            get
            {
                return _orderItemRepository ??
                       (_orderItemRepository = new GenericRepository<OrderItem>(_gameStoreDbContext));
            }
        }

        public IGenericRepository<Basket> BasketRepository
        {
            get
            {
                return _basketRepository ?? (_basketRepository = new GenericRepository<Basket>(_gameStoreDbContext));
            }
        }

        public IGenericRepository<BasketItem> BasketItemRepository
        {
            get
            {
                return _basketItemRepository ??
                       (_basketItemRepository = new GenericRepository<BasketItem>(_gameStoreDbContext));
            }
        }

        public IGenericRepository<Publisher> PublisherRepository
        {
            get
            {
                return _publisherRepository ??
                       (_publisherRepository = new GenericRepository<Publisher>(_gameStoreDbContext));
            }
        }

        public IGenericRepository<Shipper> ShipperRepository
        {
            get
            {
                return _shipperRepository ?? (_shipperRepository = new GenericRepository<Shipper>(_northwindDbContext));
            }
        }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new GenericRepository<User>(_gameStoreDbContext));
            }
        }

        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                return _roleRepository ?? (_roleRepository = new GenericRepository<Role>(_gameStoreDbContext));
            }
        }

        public void SaveChanges()
        {
            _gameStoreDbContext.SaveChanges();
        }
    }
}