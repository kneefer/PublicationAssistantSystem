using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PublicationAssistantSystem.DAL.Context
{
    public interface IPublicationAssistantContext
    {
        DbSet<TEntity> Set<TEntity>() 
            where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) 
            where TEntity : class;

        int SaveChanges();
    }
}