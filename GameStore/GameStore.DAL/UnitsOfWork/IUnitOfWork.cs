using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.UnitsOfWork
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; }
        IGenericRepository<Genre> GenreRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<PlatformType> PlatformTypeRepository { get; }
        IGenericRepository<Basket> BasketRepository { get; }
        IGenericRepository<OrderItem> OrderItemRepository { get; }
        IGenericRepository<OrderDetails> OrderDetailsRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<Publisher> PublisherRepository { get; }

        void Save();
    }
}