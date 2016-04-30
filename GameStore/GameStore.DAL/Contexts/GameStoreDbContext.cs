using System.Data.Entity;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
using GameStore.DAL.Entities.Security;

namespace GameStore.DAL.Contexts
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext()
            : base("name=DbConnectionString")
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Language> Languages { get; set; }
        public DbSet<GameLocalization> GameLocalizations { get; set; }
        public DbSet<GenreLocalization> GenreLocalizations { get; set; }
        public DbSet<PublisherLocalization> PublisherLocalizations { get; set; }
        public DbSet<PlatformTypeLocalization> PlatformTypeLocalizations { get; set; }

        static GameStoreDbContext()
        {
            Database.SetInitializer(new GameStoreDbInitializer());
        }
    }
}