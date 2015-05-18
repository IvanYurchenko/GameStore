using System.Data.Entity;
using System.Linq;
using System.Reflection;
using AutoMapper;
using GameStore.DAL.Comparing;
using GameStore.DAL.Contexts;
using GameStore.DAL.Entities;
using GameStore.DAL.Extentions;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind;

namespace GameStore.DAL.Synchronizing
{
    public class GenericSynchronizer<TEntity, TNorthwindEntity>
        where TEntity : SyncEntity 
        where TNorthwindEntity : class
    {
        private readonly GameStoreDbContext _gameStoreDbContext;
        private readonly NorthwindDbContext _northwindDbContext;

        private readonly ICustomComparerFactory _comparerFactory;

        public GenericSynchronizer(GameStoreDbContext gameStoreDbContext, NorthwindDbContext northwindDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _northwindDbContext = northwindDbContext;

            _comparerFactory = new CustomComparerFactory();
        }

        public void Synchronize(TNorthwindEntity northwindEntity)
        {
            DbSet<TEntity> tEntityDbSet = _gameStoreDbContext.Set<TEntity>();

            if (northwindEntity.IsProxy())
            {
                northwindEntity = northwindEntity.UnProxy(_northwindDbContext);
            }

            TEntity entityFromNorthwind = Mapper.Map<TEntity>(northwindEntity);

            string northwindIdPropertyName = typeof (TNorthwindEntity).IdentifierPropertyName();
            var northwindEntityId = (int) northwindEntity.GetPropValue(northwindIdPropertyName);

            TEntity gameStoreEntity = tEntityDbSet
                .FirstOrDefault(x => x.NorthwindId == northwindEntityId);

            if (gameStoreEntity == null)
            {
                tEntityDbSet.Add(entityFromNorthwind);
            }
            else
            {
                if (gameStoreEntity.IsProxy())
                {
                    gameStoreEntity = gameStoreEntity.UnProxy(_gameStoreDbContext);
                }

                string gameStoreIdPropertyName = typeof (TEntity).IdentifierPropertyName();
                PropertyInfo idPropertyInfo = typeof (TEntity).GetProperty(gameStoreIdPropertyName);
                var gameStoreEntityId = (int) gameStoreEntity.GetPropValue(gameStoreIdPropertyName);
                idPropertyInfo.SetValue(entityFromNorthwind, gameStoreEntityId, null);

                ICustomComparer comparer = _comparerFactory.GetComparer(typeof (TEntity));

                if (!comparer.AreEqual(gameStoreEntity, entityFromNorthwind))
                {
                    _gameStoreDbContext.Entry(gameStoreEntity).CurrentValues.SetValues(entityFromNorthwind);
                }
            }
        }
    }
}