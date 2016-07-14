using System.Data.Entity;
using System.Linq;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Northwind;

namespace GameStore.DAL.Synchronizing
{
    public class GenericCleaner<TEntity, TNorthwindEntity> where TEntity : SyncEntity where TNorthwindEntity : class
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        public GenericCleaner(GameStoreDbContext gameStoreDbContext, NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;
        }

        public void DeleteUnusedEntities()
        {
            DbSet<TEntity> gameStoreDbSet = _gameStoreDbContext.Set<TEntity>();
            DbSet<TNorthwindEntity> northwindDbSet = _northwindDbContext.Set<TNorthwindEntity>();

            foreach (TEntity entity in gameStoreDbSet.Where(x => x.NorthwindId != null))
            {
                if (northwindDbSet.Find(entity.NorthwindId) == null)
                {
                    gameStoreDbSet.Remove(entity);
                }
            }
        }
    }
}