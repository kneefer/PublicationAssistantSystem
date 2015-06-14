using System.Linq;
using PublicationAssistantSystem.Core.SearchApiEngines;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.Core.PostAddJobs
{
    public class PublicationsJobs : PostAddJobsBase<PublicationBase>
    {
        private bool _isOnWOS = false;

        public PublicationsJobs(PublicationBase publicationToModify)
            : base(publicationToModify) { }

        protected override void GetModifications()
        {
            var toModify = EntityToModify;

            var results = new WebOfScienceSearchEngine()
                .ByTitle(toModify.Title)
                .GetResults().ToList();

            if (!results.Any()) return;

            if (results.Count() == 1)
            {
                _isOnWOS = true;
                return;
            }

            results = new WebOfScienceSearchEngine()
                .ByTitle(toModify.Title)
                .ByAuthors(toModify.Employees.Select(x => x.LastName).ToArray())
                .GetResults().ToList();

            if (results.Count() == 1)
                _isOnWOS = true;
        }

        protected override void SetModifications(PublicationBase toModify)
        {
            toModify.IsOnWOS = _isOnWOS;
        }
    }
}
