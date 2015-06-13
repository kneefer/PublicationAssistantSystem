using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.Core.PostAddJobs
{
    public class JournalsJobs : PostAddJobsBase<Journal>
    {
        private bool _isOnJCR;
        private bool _isOnMNISZW;
        private bool _isOnWOS;

        public JournalsJobs(Journal journalToModify)
            : base(journalToModify) { }

        protected override void GetModifications()
        {
            var toModify = EntityToModify;

            //TODO Replace with real checking 
            _isOnJCR = true;
            _isOnMNISZW = true;
            _isOnWOS = true;
        }

        protected override void SetModifications(Journal toModify)
        {
            toModify.IsOnJCR    = _isOnJCR;
            toModify.IsOnMNISZW = _isOnMNISZW;
            toModify.IsOnWOS    = _isOnWOS;
        }
    }
}
