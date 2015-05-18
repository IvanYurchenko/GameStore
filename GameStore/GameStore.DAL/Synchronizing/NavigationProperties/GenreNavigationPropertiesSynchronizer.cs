using System.Collections.Generic;
using System.Data;
using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Northwind;

namespace GameStore.DAL.Synchronizing.NavigationProperties
{
    public class GenreNavigationPropertiesSynchronizer
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        public GenreNavigationPropertiesSynchronizer(GameStoreDbContext gameStoreDbContext,
            NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;
        }

        public void SynchronizeNavigationProperties(Genre genre)
        {
            Category category = _northwindDbContext.Categories.First(x => x.CategoryID == genre.NorthwindId);

            genre.Games = new List<Game>();

            foreach (var product in category.Products)
            {
                var game = _gameStoreDbContext.Set<Game>()
                    .FirstOrDefault(g => g.NorthwindId == product.ProductID);
                if (game != null)
                {
                    genre.Games.Add(game);
                }
            }

            if (_gameStoreDbContext.Entry(genre).State == EntityState.Detached)
            {
                _gameStoreDbContext.Set<Genre>().Attach(genre);
            }

            _gameStoreDbContext.Entry(genre).State = EntityState.Modified;
        }
    }
}