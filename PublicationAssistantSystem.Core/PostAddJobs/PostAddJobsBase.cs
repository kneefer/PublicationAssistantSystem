using System.Threading;
using System.Threading.Tasks;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Models;

namespace PublicationAssistantSystem.Core.PostAddJobs
{
    public abstract class PostAddJobsBase<TEntity>
        where TEntity : class, IComputableEntity 
    {
        private readonly int _entityId;
        protected readonly TEntity EntityToModify;

        protected PostAddJobsBase(TEntity entityToModify)
        {
            EntityToModify = entityToModify;
            _entityId = entityToModify.Id;
        }

        public void Start()
        {
            Task.Run(() =>
            {
                GetModifications();
                Save();
            });
        }

        protected abstract void GetModifications();

        protected abstract void SetModifications(TEntity toModify);

        private void Save()
        {
            using (var context = new PublicationAssistantContext())
            {
                var toModify = context.Set<TEntity>().Find(_entityId);
                SetModifications(toModify);

                toModify.IsComputing = false;
                context.SaveChanges();
            }
        }
    }
}
