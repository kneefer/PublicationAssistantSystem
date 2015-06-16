using System.Linq;
using PublicationAssistantSystem.Core.SearchApiEngines;
using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.Core.PostAddJobs
{
    public class JournalsJobs : PostAddJobsBase<Journal>
    {
        private bool _isOnJCR;
        private bool _isOnMNISZW;
        private bool _isOnWOS;
        private string _mniszwList;
        private int _mniszwPoints;

        public JournalsJobs(Journal journalToModify)
            : base(journalToModify) { }

        protected override void GetModifications()
        {
            var toModify = EntityToModify;

            // WebOfScience existance checking
            var results = new WebOfScienceSearchEngine()
                .ByISBNISSN(toModify.ISSN)
                .GetResults();

            if (results.Any())
                _isOnWOS = true;

            // MNiSzW existance checking
            results = new MniszwSearchEngine()
                .ByISBNISSN(toModify.ISSN)
                .GetResults();

            if (results.Any())
                _isOnMNISZW = true;
        }

        protected override void SetModifications(Journal toModify)
        {
            toModify.IsOnJCR      = _isOnJCR;
            toModify.IsOnMNISZW   = _isOnMNISZW;
            toModify.IsOnWOS      = _isOnWOS;
            toModify.MNISZWList   = _mniszwList;
            toModify.MNISZWPoints = _mniszwPoints;
        }
    }
}
