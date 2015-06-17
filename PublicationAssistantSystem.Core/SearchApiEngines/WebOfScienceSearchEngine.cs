using System;
using System.Collections.Generic;
using System.Linq;
using PublicationAssistantSystem.Core.Clients;
using PublicationAssistantSystem.Core.Mappers.Common;
using PublicationAssistantSystem.Core.Mappers.WOS;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.Core.SearchApiEngines
{
    public class WebOfScienceSearchEngine
    {
        private readonly List<string> _queryStrings = new List<string>(); 

        private bool IsReadyToQuery
        {
            get { return _queryStrings.Count > 0; }
        }

        public IEnumerable<IRecord> GetResults()
        {
            if (IsReadyToQuery)
                return RunQuery();

            throw new ArgumentException("Engine is not ready to search.");
        }

        private IEnumerable<IRecord> RunQuery()
        {
            var mapper = new WOSRecordToIRecordConverter();

            var result = RunRawQuery();
            var mappedResult = mapper.ToIRecord(result);
            return mappedResult;
        }

        public WebOfScienceSearchEngine ByTitle(string title)
        {
            _queryStrings.Add(string.Format("TI={0}", title));
            return this;
        }

        public WebOfScienceSearchEngine ByISBNISSN(string isbnOrIssn)
        {
            _queryStrings.Add(string.Format("IS={0}", isbnOrIssn));
            return this;
        }

        public WebOfScienceSearchEngine ByAuthors(string[] authorsSecondNames)
        {
            foreach (var authorSecondName in authorsSecondNames)
            {
                _queryStrings.Add(string.Format("AU={0}", authorSecondName));
            }
            return this;
        }

        private searchResults RunRawQuery()
        {
            var authClient = new AuthenticationClient();

            try
            {
                var sessionIdentifier = authClient.authenticate();
                var searchClient = new SearchClient(sessionIdentifier);

                var results = searchClient.search(new queryParameters
                {
                    databaseId = "WOS",
                    editions = new[]
                    {
                        new editionDesc 
                        { 
                            collection = "WOS", 
                            edition = "SCI" 
                        }
                    },
                    queryLanguage = "en",
                    userQuery = _queryStrings.Aggregate((x,y) => x + " AND " + y),
                },
                new retrieveParameters
                {
                    count = 10,
                    firstRecord = 1,
                });
                authClient.closeSession();

                return results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
