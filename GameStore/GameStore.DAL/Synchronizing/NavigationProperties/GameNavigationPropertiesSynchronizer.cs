using System.Data;
using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Northwind;

namespace GameStore.DAL.Synchronizing.NavigationProperties
{
    public class GameNavigationPropertiesSynchronizer
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        public GameNavigationPropertiesSynchronizer(GameStoreDbContext gameStoreDbContext,
            NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;
        }

        public void SynchronizeNavigationProperties(Game game)
        {
            Product product = _northwindDbContext.Products.First(x => x.ProductID == game.NorthwindId);

            game.Genres = _gameStoreDbContext.Set<Genre>()
                .Where(x => x.NorthwindId == product.Category.CategoryID)
                .ToList();

            game.Publisher = _gameStoreDbContext.Set<Publisher>()
                .FirstOrDefault(x => x.NorthwindId == product.Supplier.SupplierID);

            if (game.Publisher != null)
            {
                game.PublisherId = game.Publisher.PublisherId;
            }

            game.BasketItems = _gameStoreDbContext.Set<BasketItem>()
                .Where(bi => bi.GameId == game.GameId)
                .ToList();

            game.Comments = _gameStoreDbContext.Set<Comment>()
                .Where(c => c.GameId == game.GameId)
                .ToList();

            if (_gameStoreDbContext.Entry(game).State == EntityState.Detached)
            {
                _gameStoreDbContext.Set<Game>().Attach(game);
            }

            _gameStoreDbContext.Entry(game).State = EntityState.Modified;
        }
    }
}