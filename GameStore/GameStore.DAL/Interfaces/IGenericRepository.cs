using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetMany(Func<TEntity, bool> filterCondition, Func<TEntity, object> sortCondition);

        TEntity GetById(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);

        int GetCount();
    }
}