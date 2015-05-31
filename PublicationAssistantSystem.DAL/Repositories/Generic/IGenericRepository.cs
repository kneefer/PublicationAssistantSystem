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
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, TProperty>> navProperty);

        List<TEntity> Get<TProperty1, TProperty2>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, TProperty1>> navProperty1,
            Expression<Func<TEntity, TProperty2>> navProperty2);

        List<TTargetEntity> GetOfType<TTargetEntity>(
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null);

        List<TTargetEntity> GetOfType<TTargetEntity, TProperty>(
            Expression<Func<TTargetEntity, bool>> filter,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy,
            Expression<Func<TTargetEntity, TProperty>> navProperty);

        List<TTargetEntity> GetOfType<TTargetEntity, TProperty1, TProperty2>(
            Expression<Func<TTargetEntity, bool>> filter,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy,
            Expression<Func<TTargetEntity, TProperty1>> navProperty1,
            Expression<Func<TTargetEntity, TProperty2>> navProperty2);

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(int id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}