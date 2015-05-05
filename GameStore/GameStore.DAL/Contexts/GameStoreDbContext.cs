using System.Data.Entity;
using GameStore.DAL.Entities;

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
        public DbSet<OrderItem> OrderDetails { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        static GameStoreDbContext()
        {
            Database.SetInitializer(new GameStoreDbInitializer());
        }
    }
}