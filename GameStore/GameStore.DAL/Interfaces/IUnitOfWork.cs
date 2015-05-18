using GameStore.DAL.Entities;
using GameStore.DAL.Northwind;
using Order = GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }
        IGenericRepository<Genre> GenreRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<PlatformType> PlatformTypeRepository { get; }
        IGenericRepository<Basket> BasketRepository { get; }
        IGenericRepository<BasketItem> BasketItemRepository { get; }
        IGenericRepository<OrderItem> OrderItemRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<Publisher> PublisherRepository { get; }
        IGenericRepository<Shipper> ShipperRepository { get; }

        /// <summary>
        /// Calls the SaveChanges method of the db context.
        /// </summary>
        void SaveChanges();
    }
}