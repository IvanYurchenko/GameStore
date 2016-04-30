using System.Collections.Generic;
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

            game.Genres = new List<Genre>();

            foreach (Genre genre in _gameStoreDbContext.Set<Genre>()
                .Where(x => x.NorthwindId == product.Category.CategoryID)
                .ToList())
            {
                game.Genres.Add(genre);
            }

            game.Publisher = _gameStoreDbContext.Set<Publisher>()
                .FirstOrDefault(x => x.NorthwindId == product.Supplier.SupplierID);

            if (game.Publisher != null)
            {
                game.PublisherId = game.Publisher.PublisherId;
            }

            game.BasketItems = new List<BasketItem>();

            foreach (BasketItem basketItem in _gameStoreDbContext.Set<BasketItem>()
                .Where(bi => bi.GameId == game.GameId)
                .ToList())
            {
                game.BasketItems.Add(basketItem);
            }

            game.Comments = new List<Comment>();

            foreach (Comment comment in _gameStoreDbContext.Set<Comment>()
                .Where(c => c.GameId == game.GameId)
                .ToList())
            {
                game.Comments.Add(comment);
            }

            if (_gameStoreDbContext.Entry(game).State == EntityState.Detached)
            {
                _gameStoreDbContext.Set<Game>().Attach(game);
            }

            _gameStoreDbContext.Entry(game).State = EntityState.Modified;
        }
    }
}