using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.Core.PostAddJobs
{
    public class PublicationsJobs : PostAddJobsBase<PublicationBase>
    {
        private bool _isOnWOS;

        public PublicationsJobs(PublicationBase publicationToModify)
            : base(publicationToModify) { }

        protected override void GetModifications()
        {
            var toModify = EntityToModify;

            //TODO Replace with real checking 
            _isOnWOS = true;
        }

        protected override void SetModifications(PublicationBase toModify)
        {
            toModify.IsOnWOS = _isOnWOS;
        }
    }
}
