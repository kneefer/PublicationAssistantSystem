using System;
using System.Net;

namespace PublicationAssistantSystem.Core
{
    public class Client : WebOfKnowledgeApi.Authentication.WOKMWSAuthenticateService
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            var request = base.GetWebRequest(uri);
            request.Headers.Add("Authorization", Test.Cred);
            return request;
        }
    }
}