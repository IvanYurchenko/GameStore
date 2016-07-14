using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
using GameStore.DAL.Entities.Security;
using GameStore.DAL.Northwind;
using Order = GameStore.DAL.Entities.Order;

namespace GameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the game repository.
        /// </summary>
        /// <value>
        /// The game repository.
        /// </value>
        IGameRepository GameRepository { get; }

        /// <summary>
        /// Gets the genre repository.
        /// </summary>
        /// <value>
        /// The genre repository.
        /// </value>
        IGenericRepository<Genre> GenreRepository { get; }

        /// <summary>
        /// Gets the comment repository.
        /// </summary>
        /// <value>
        /// The comment repository.
        /// </value>
        IGenericRepository<Comment> CommentRepository { get; }

        /// <summary>
        /// Gets the platform type repository.
        /// </summary>
        /// <value>
        /// The platform type repository.
        /// </value>
        IGenericRepository<PlatformType> PlatformTypeRepository { get; }

        /// <summary>
        /// Gets the basket repository.
        /// </summary>
        /// <value>
        /// The basket repository.
        /// </value>
        IGenericRepository<Basket> BasketRepository { get; }

        /// <summary>
        /// Gets the basket item repository.
        /// </summary>
        /// <value>
        /// The basket item repository.
        /// </value>
        IGenericRepository<BasketItem> BasketItemRepository { get; }

        /// <summary>
        /// Gets the order item repository.
        /// </summary>
        /// <value>
        /// The order item repository.
        /// </value>
        IGenericRepository<OrderItem> OrderItemRepository { get; }

        /// <summary>
        /// Gets the order repository.
        /// </summary>
        /// <value>
        /// The order repository.
        /// </value>
        IGenericRepository<Order> OrderRepository { get; }

        /// <summary>
        /// Gets the publisher repository.
        /// </summary>
        /// <value>
        /// The publisher repository.
        /// </value>
        IGenericRepository<Publisher> PublisherRepository { get; }

        /// <summary>
        /// Gets the shipper repository.
        /// </summary>
        /// <value>
        /// The shipper repository.
        /// </value>
        IGenericRepository<Shipper> ShipperRepository { get; }

        /// <summary>
        /// Gets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        IGenericRepository<User> UserRepository { get; }

        /// <summary>
        /// Gets the role repository.
        /// </summary>
        /// <value>
        /// The role repository.
        /// </value>
        IGenericRepository<Role> RoleRepository { get; }

        /// <summary>
        /// Gets the language repository.
        /// </summary>
        /// <value>
        /// The language repository.
        /// </value>
        IGenericRepository<Language> LanguageRepository { get; }

        /// <summary>
        /// Gets the game localization repository.
        /// </summary>
        /// <value>
        /// The game localization repository.
        /// </value>
        IGenericRepository<GameLocalization> GameLocalizationRepository { get; }

        /// <summary>
        /// Gets the genre localization repository.
        /// </summary>
        /// <value>
        /// The genre localization repository.
        /// </value>
        IGenericRepository<GenreLocalization> GenreLocalizationRepository { get; }

        /// <summary>
        /// Gets the platform type localization repository.
        /// </summary>
        /// <value>
        /// The platform type localization repository.
        /// </value>
        IGenericRepository<PlatformTypeLocalization> PlatformTypeLocalizationRepository { get; }

        /// <summary>
        /// Gets the publisher localization repository.
        /// </summary>
        /// <value>
        /// The publisher localization repository.
        /// </value>
        IGenericRepository<PublisherLocalization> PublisherLocalizationRepository { get; }

        /// <summary>
        /// Calls the SaveChanges method of the db context.
        /// </summary>
        void SaveChanges();
    }
}