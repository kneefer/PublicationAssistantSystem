using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PublicationAssistantSystem.DAL.Context;

namespace PublicationAssistantSystem.DAL.Repositories.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class
    {
        private readonly IPublicationAssistantContext _context;
        private readonly IDbSet<TEntity> _dbSet;

        public GenericRepository(IPublicationAssistantContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        private static List<TTargetEntity> Get<TTargetEntity>(
            IQueryable<TTargetEntity> query,
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null)
        {
            if (filter != null)
                query = query.Where(filter);

            return orderBy != null
                ? orderBy(query).ToList()
                : query.ToList();
        }

        public List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;
            return Get(query, filter, orderBy);
        }

        public virtual List<TEntity> Get<TProperty>(
            Expression<Func<TEntity, bool>> filter, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, 
            Expression<Func<TEntity, TProperty>> navProperty)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Include(navProperty);

            return Get(query, filter, orderBy);
        }

        public virtual List<TEntity> Get<TProperty1, TProperty2>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, TProperty1>> navProperty1,
            Expression<Func<TEntity, TProperty2>> navProperty2)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Include(navProperty1);
            query = query.Include(navProperty2);

            return Get(query, filter, orderBy);
        }

        public virtual List<TTargetEntity> GetOfType<TTargetEntity>(
            Expression<Func<TTargetEntity, bool>> filter = null, 
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null)
        {
            IQueryable<TTargetEntity> query = _dbSet.OfType<TTargetEntity>();
            return Get(query, filter, orderBy);
        }

        public virtual List<TTargetEntity> GetOfType<TTargetEntity, TProperty>(
            Expression<Func<TTargetEntity, bool>> filter, 
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy, 
            Expression<Func<TTargetEntity, TProperty>> navProperty)
        {
            IQueryable<TTargetEntity> query = _dbSet.OfType<TTargetEntity>();

            query = query.Include(navProperty);

            return Get(query, filter, orderBy);
        }

        public virtual List<TTargetEntity> GetOfType<TTargetEntity, TProperty1, TProperty2>(
            Expression<Func<TTargetEntity, bool>> filter,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy,
            Expression<Func<TTargetEntity, TProperty1>> navProperty1,
            Expression<Func<TTargetEntity, TProperty2>> navProperty2)
        {
            IQueryable<TTargetEntity> query = _dbSet.OfType<TTargetEntity>();

            query = query.Include(navProperty1);
            query = query.Include(navProperty2);

            return Get(query, filter, orderBy);
        }

        public virtual TEntity GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
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
    }
}