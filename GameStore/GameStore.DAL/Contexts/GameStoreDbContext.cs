using System.Data.Entity;
using GameStore.DAL.Entities;
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

        static GameStoreDbContext()
        {
            Database.SetInitializer(new GameStoreDbInitializer());
        }
    }
}