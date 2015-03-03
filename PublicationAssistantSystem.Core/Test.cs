using System;
using System.Security;
using System.Text;
using PublicationAssistantSystem.Core.WebOfKnowledgeApi.Search;

namespace PublicationAssistantSystem.Core
{
    public class Test
    {
        public const string UserName = "kneefer@gmail.com";
        public const string Password = "P@ssw0rd";
        public static string Cred { get; private set; }

        static Test()
        {
            Cred = ConvertCredentialsToBasicAccessFormat(UserName, Password);
        }

        public void Run()
        {
            var x = new Client();
            x.authenticate();
        }

        public static string ConvertCredentialsToBasicAccessFormat(string userName, string password)
        {
            var credentials = string.Format("{0}:{1}", userName, password);
            var convertedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            return "Basic " + convertedCredentials;
        }
    }
}
