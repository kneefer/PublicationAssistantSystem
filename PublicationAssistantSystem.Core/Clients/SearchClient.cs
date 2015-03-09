using System;
using System.Net;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.Core.Clients
{
    public class SearchClient : WokSearchLiteService
    {
        public string SessionIdentifier { get; private set; }

        public SearchClient(string sessionIdentifier)
        {
            SessionIdentifier = sessionIdentifier;
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            var request = base.GetWebRequest(uri);
            request.Headers.Add("Cookie", string.Format("SID=\"{0}\"", SessionIdentifier));
            return request;
        }
    }
}
