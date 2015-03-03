using System;
using System.Net;

namespace PublicationAssistantSystem.Core
{
    public class Client : com.webofknowledge.search.WOKMWSAuthenticateService
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            var request = base.GetWebRequest(uri);
            request.Headers.Add("Authorization", Test.CredentialsBased);
            return request;
        }
    }
}