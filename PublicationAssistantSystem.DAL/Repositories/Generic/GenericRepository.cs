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
        private readonly IDbSet<TEntity> _dbSet;
        private readonly IPublicationAssistantContext _context;

        public GenericRepository(IPublicationAssistantContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #region Get
        
        #region Get base methods
        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        private static List<TTargetEntity> Get<TTargetEntity>(
           IQueryable<TTargetEntity> query,
           Expression<Func<TTargetEntity, bool>> filter,
           Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null)
        {
            if (filter != null)
                query = query.Where(filter);

            return orderBy != null
                ? orderBy(query).ToList()
                : query.ToList();
        }

        private static List<TTargetEntity> Get<TTargetEntity>(
            IQueryable<TTargetEntity> query,
            IList<Expression<Func<TTargetEntity, bool>>> filters,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null)
        {
            if (filters == null)
                filters = new List<Expression<Func<TTargetEntity, bool>>>();

            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }

            return orderBy != null
                ? orderBy(query).ToList()
                : query.ToList();
        }

        #endregion Get base methods

        public virtual List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            return Get(query, filter, orderBy);
        }

        public virtual List<TEntity> Get(
            IList<Expression<Func<TEntity, bool>>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;
            return Get(query, filters, orderBy);
        }


        public virtual List<TEntity> Get<TProperty>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TProperty>> navProperty = null)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Include(navProperty);

            return Get(query, filter, orderBy);
        }

        public virtual List<TEntity> Get<TProperty>(
            IList<Expression<Func<TEntity, bool>>> filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TProperty>> navProperty = null)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Include(navProperty);

            return Get(query, filters, orderBy);
        }


        public virtual List<TEntity> Get<TProperty1, TProperty2>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TProperty1>> navProperty1 = null,
            Expression<Func<TEntity, TProperty2>> navProperty2 = null)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Include(navProperty1);
            query = query.Include(navProperty2);

            return Get(query, filter, orderBy);
        }

        public virtual List<TEntity> Get<TProperty1, TProperty2>(
            IList<Expression<Func<TEntity, bool>>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, TProperty1>> navProperty1,
            Expression<Func<TEntity, TProperty2>> navProperty2)
        {
            IQueryable<TEntity> query = _dbSet;

            query = query.Include(navProperty1);
            query = query.Include(navProperty2);

            return Get(query, filters, orderBy);
        }


        public virtual List<TTargetEntity> GetOfType<TTargetEntity>(
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>,IOrderedQueryable<TTargetEntity>> orderBy = null)
        {
            var query = _dbSet.OfType<TTargetEntity>();
            return Get(query, filter, orderBy);
        }

        public virtual List<TTargetEntity> GetOfType<TTargetEntity>(
            IList<Expression<Func<TTargetEntity, bool>>> filters = null, 
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null)
        {
            var query = _dbSet.OfType<TTargetEntity>();
            return Get(query, filters, orderBy);
        }


        public virtual List<TTargetEntity> GetOfType<TTargetEntity, TProperty>(
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null,
            Expression<Func<TTargetEntity, TProperty>> navProperty = null)
        {
            var query = _dbSet.OfType<TTargetEntity>();

            query = query.Include(navProperty);

            return Get(query, filter, orderBy);
        }

        public virtual List<TTargetEntity> GetOfType<TTargetEntity, TProperty>(
            IList<Expression<Func<TTargetEntity, bool>>> filters, 
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy, 
            Expression<Func<TTargetEntity, TProperty>> navProperty)
        {
            var query = _dbSet.OfType<TTargetEntity>();

            query = query.Include(navProperty);

            return Get(query, filters, orderBy);
        }


        public virtual List<TTargetEntity> GetOfType<TTargetEntity, TProperty1, TProperty2>(
            Expression<Func<TTargetEntity, bool>> filter = null, 
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null,
            Expression<Func<TTargetEntity, TProperty1>> navProperty1 = null, 
            Expression<Func<TTargetEntity, TProperty2>> navProperty2 = null)
        {
            var query = _dbSet.OfType<TTargetEntity>();

            query = query.Include(navProperty1);
            query = query.Include(navProperty2);

            return Get(query, filter, orderBy);
        }

        public virtual List<TTargetEntity> GetOfType<TTargetEntity, TProperty1, TProperty2>(
            IList<Expression<Func<TTargetEntity, bool>>> filters,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy,
            Expression<Func<TTargetEntity, TProperty1>> navProperty1,
            Expression<Func<TTargetEntity, TProperty2>> navProperty2)
        {
            var query = _dbSet.OfType<TTargetEntity>();

            query = query.Include(navProperty1);
            query = query.Include(navProperty2);

            return Get(query, filters, orderBy);
        }

        #endregion Get

        #region Insert

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        #endregion Insert

        #region Update

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        #endregion Update

        #region Delete

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

        #endregion Delete
    }
}