using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PublicationAssistantSystem.DAL.Repositories.Generic
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        List<TEntity> Get<TProperty>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, TProperty>>[] navProperty);

        List<TTargetEntity> GetOfType<TTargetEntity>(
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null);

        List<TTargetEntity> GetOfType<TTargetEntity, TProperty>(
            Expression<Func<TTargetEntity, bool>> filter,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy,
            params Expression<Func<TTargetEntity, TProperty>>[] navProperties);

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(int id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}