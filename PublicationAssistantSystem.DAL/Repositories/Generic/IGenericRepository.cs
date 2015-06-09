using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PublicationAssistantSystem.DAL.Repositories.Generic
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        #region Get

        TEntity GetById(object id);

        List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        List<TEntity> Get(
            IList<Expression<Func<TEntity, bool>>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);


        List<TEntity> Get<TProperty>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TProperty>> navProperty = null);

        List<TEntity> Get<TProperty>(
            IList<Expression<Func<TEntity, bool>>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TProperty>> navProperty = null);


        List<TEntity> Get<TProperty1, TProperty2>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TProperty1>> navProperty1 = null,
            Expression<Func<TEntity, TProperty2>> navProperty2 = null);

        List<TEntity> Get<TProperty1, TProperty2>(
            IList<Expression<Func<TEntity, bool>>> filters,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, TProperty1>> navProperty1 = null,
            Expression<Func<TEntity, TProperty2>> navProperty2 = null);


        List<TTargetEntity> GetOfType<TTargetEntity>(
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null);

        List<TTargetEntity> GetOfType<TTargetEntity>(
            IList<Expression<Func<TTargetEntity, bool>>> filters,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null);


        List<TTargetEntity> GetOfType<TTargetEntity, TProperty>(
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null,
            Expression<Func<TTargetEntity, TProperty>> navProperty = null);
        
        List<TTargetEntity> GetOfType<TTargetEntity, TProperty>(
            IList<Expression<Func<TTargetEntity, bool>>> filters,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null,
            Expression<Func<TTargetEntity, TProperty>> navProperty = null);


        List<TTargetEntity> GetOfType<TTargetEntity, TProperty1, TProperty2>(
            Expression<Func<TTargetEntity, bool>> filter = null,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null,
            Expression<Func<TTargetEntity, TProperty1>> navProperty1 = null,
            Expression<Func<TTargetEntity, TProperty2>> navProperty2 = null);
        List<TTargetEntity> GetOfType<TTargetEntity, TProperty1, TProperty2>(
            IList<Expression<Func<TTargetEntity, bool>>> filters,
            Func<IQueryable<TTargetEntity>, IOrderedQueryable<TTargetEntity>> orderBy = null,
            Expression<Func<TTargetEntity, TProperty1>> navProperty1 = null,
            Expression<Func<TTargetEntity, TProperty2>> navProperty2 = null);

        #endregion Get

        #region Insert

        void Insert(TEntity entity);

        #endregion Insert

        #region Update

        void Update(TEntity entityToUpdate);

        #endregion Update

        #region Delete

        void Delete(int id);

        void Delete(TEntity entityToDelete);

        #endregion Delete
    }
}