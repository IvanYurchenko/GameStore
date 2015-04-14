using System;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;

namespace GameStore.DAL.UnitsOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly MyDbContext _context = new MyDbContext();

        #region Private fields

        private GameRepository _gameRepository;
        private GenericRepository<Genre> _genreRepository;
        private GenericRepository<Comment> _commentRepository;
        private GenericRepository<PlatformType> _platformTypeRepository;
        private GenericRepository<Basket> _basketRepository;
        private GenericRepository<OrderItem> _orderItemRepository; 
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetails> _orderDetailsRepository;

        #endregion

        #region Public properties

        public IGameRepository GameRepository
        {
            get
            {

                if (_gameRepository == null)
                {
                    _gameRepository = new GameRepository(_context);
                }
                return _gameRepository;
            }
        }

        public IGenericRepository<Genre> GenreRepository
        {
            get
            {

                if (_genreRepository == null)
                {
                    _genreRepository = new GenericRepository<Genre>(_context);
                }
                return _genreRepository;
            }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get
            {

                if (_commentRepository == null)
                {
                    _commentRepository = new GenericRepository<Comment>(_context);
                }
                return _commentRepository;
            }
        }

        public IGenericRepository<PlatformType> PlatformTypeRepository
        {
            get
            {

                if (_platformTypeRepository == null)
                {
                    _platformTypeRepository = new GenericRepository<PlatformType>(_context);
                }
                return _platformTypeRepository;
            }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        public IGenericRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {
                if (_orderDetailsRepository == null)
                {
                    _orderDetailsRepository = new GenericRepository<OrderDetails>(_context);
                }
                return _orderDetailsRepository;
            }
        }

   public IGenericRepository<Basket> BasketRepository
        {
            get
            {

                if (_basketRepository == null)
                {
                    _basketRepository = new GenericRepository<Basket>(_context);
                }
                return _basketRepository;
            }
        }

        public IGenericRepository<OrderItem> OrderItemRepository
        {
            get
            {

                if (_orderItemRepository == null)
                {
                    _orderItemRepository = new GenericRepository<OrderItem>(_context);
                }
                return _orderItemRepository;
            }
        }
        
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}