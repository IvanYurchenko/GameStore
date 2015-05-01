using System;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Repositories;

namespace GameStore.DAL.UnitsOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly GameStoreDbContext _context = new GameStoreDbContext();

        #region Private fields

        private GameRepository _gameRepository;
        private GenericRepository<Genre> _genreRepository;
        private GenericRepository<Comment> _commentRepository;
        private GenericRepository<PlatformType> _platformTypeRepository;
        private GenericRepository<Basket> _basketRepository;
        private GenericRepository<BasketItem> _basketItemRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetail> _orderDetailRepository;
        private GenericRepository<Publisher> _publisherRepository;

        #endregion

        #region Public properties

        public IGameRepository GameRepository
        {
            get { return _gameRepository ?? (_gameRepository = new GameRepository(_context)); }
        }

        public IGenericRepository<Genre> GenreRepository
        {
            get { return _genreRepository ?? (_genreRepository = new GenericRepository<Genre>(_context)); }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get { return _commentRepository ?? (_commentRepository = new GenericRepository<Comment>(_context)); }
        }

        public IGenericRepository<PlatformType> PlatformTypeRepository
        {
            get
            {
                return _platformTypeRepository ??
                       (_platformTypeRepository = new GenericRepository<PlatformType>(_context));
            }
        }

        public IGenericRepository<Order> OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_context)); }
        }

        public IGenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                return _orderDetailRepository ??
                       (_orderDetailRepository = new GenericRepository<OrderDetail>(_context));
            }
        }

        public IGenericRepository<Basket> BasketRepository
        {
            get { return _basketRepository ?? (_basketRepository = new GenericRepository<Basket>(_context)); }
        }

        public IGenericRepository<BasketItem> BasketItemRepository
        {
            get
            {
                return _basketItemRepository ?? (_basketItemRepository = new GenericRepository<BasketItem>(_context));
            }
        }

        public IGenericRepository<Publisher> PublisherRepository
        {
            get { return _publisherRepository ?? (_publisherRepository = new GenericRepository<Publisher>(_context)); }
        }

        #endregion

        public void SaveChanges()
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