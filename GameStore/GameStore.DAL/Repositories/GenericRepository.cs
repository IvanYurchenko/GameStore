using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> entities = _dbSet;
            return entities.ToList();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> allEntities = _dbSet;

            if (filter != null)
            {
                allEntities = allEntities.Where(filter);
            }

            return allEntities.ToList();
        }

        public virtual IEnumerable<TEntity> GetMany(
            Func<TEntity, bool> filterCondition,
            int pageCapacity,
            int pageNumber,
            Func<TEntity, object> sortCondition = null)
        {
            IEnumerable<TEntity> result = null;

            if (filterCondition != null)
            {
                if (sortCondition == null)
                {
                    result = _dbSet.Where(filterCondition)
                        .Skip(pageCapacity*(pageNumber - 1))
                        .Take(pageCapacity);
                }
                else
                {
                    result = _dbSet.Where(filterCondition)
                        .OrderBy(sortCondition)
                        .Skip(pageCapacity*(pageNumber - 1))
                        .Take(pageCapacity);
                }
            }

            return result == null ? null : result.ToList();
        }


        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual int GetCount(Func<TEntity, bool> filterCondition = null)
        {
            int result = filterCondition == null ? _dbSet.Count() : _dbSet.Count(filterCondition);
            return result;
        }
    }
}