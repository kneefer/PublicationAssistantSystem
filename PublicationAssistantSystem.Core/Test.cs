using System;
using System.Text;
using PublicationAssistantSystem.Core.Clients;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.Core
{
    public class Test
    {
        public void Run()
        {
            var x = new AuthenticationClient();

            try
            {
                var sessionIdentifier = x.authenticate();
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
                    timeSpan = new timeSpan
                    {
                        begin = "2000-01-01",
                        end = DateTime.Now.ToString("yyyy-MM-dd")
                    },
                    userQuery = "TS=(cadmium OR lead)",
                }, 
                new retrieveParameters
                {
                    count = 10,
                    firstRecord = 1,
                });
                Console.WriteLine(results.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
