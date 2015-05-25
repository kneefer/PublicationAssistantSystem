using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PublicationAssistantSystem.DAL.Repositories.Generic
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] navigationProperties);

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        //void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }

    
}
