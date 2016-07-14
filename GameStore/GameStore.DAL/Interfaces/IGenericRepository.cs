using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all entities from the database.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets entities that match the specified filter condition.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets entities with specified page and page capacity that match the specified
        /// filter condition and sorted in specified order.
        /// </summary>
        /// <param name="filterCondition">The filter condition.</param>
        /// <param name="pageCapacity">The page capacity.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="sortCondition">The sort condition.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetMany(Func<TEntity, bool> filterCondition, int pageCapacity, int pageNumber,
            Func<TEntity, object> sortCondition = null);

        /// <summary>
        /// Gets entity by it's ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Inserts the specified entity to the database.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Deletes the specified by ID entity from the database.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);

        /// <summary>
        /// Deletes the specified entity from the database.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Gets the count of entities speicifed by a filter.
        /// </summary>
        /// <param name="filterCondition">The filter condition.</param>
        /// <returns></returns>
        int GetCount(Func<TEntity, bool> filterCondition = null);
    }
}