using System;
using System.IO;
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

            if (toModify.ISSN == null && toModify.eISSN == null) return;

            SearchWOS(toModify);
            SearchMNiSzW(toModify);
        }

        private void SearchWOS(Journal toModify)
        {
            if (toModify.ISSN != null)
            {
                var results = new WebOfScienceSearchEngine()
                    .ByISBNISSN(toModify.ISSN)
                    .GetResults().ToList();

                if (results.Any())
                {
                    _isOnWOS = true;
                    return;
                }
            }

            if (toModify.eISSN != null)
            {
                var results = new WebOfScienceSearchEngine()
                    .ByISBNISSN(toModify.eISSN)
                    .GetResults().ToList();

                if (results.Any())
                {
                    _isOnWOS = true;
                }
            }
        }

        private void SearchMNiSzW(Journal toModify)
        {
            var filePath = "data.csv";

            using (var reader = new StreamReader(filePath))
            {
                if (toModify.ISSN != null)
                {
                    var results = new MniszwSearchEngine(reader)
                    .ByISSN(toModify.ISSN)
                    .GetResults().ToList();

                    if (results.Count > 1)
                        throw new Exception("Search finished with more then 1 journal result for one ISSN.");

                    if (results.SingleOrDefault() != null)
                    {
                        _isOnMNISZW = true;
                        // other shit

                        return;
                    }
                }

                if (toModify.eISSN != null)
                {
                    var results = new MniszwSearchEngine(reader)
                    .ByISSN(toModify.eISSN)
                    .GetResults().ToList();

                    if (results.Count > 1)
                        throw new Exception("Search finished with more then 1 journal result for one ISSN.");

                    if (results.SingleOrDefault() != null)
                    {
                        _isOnMNISZW = true;
                        // other shit

                    }
                }
            }
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
