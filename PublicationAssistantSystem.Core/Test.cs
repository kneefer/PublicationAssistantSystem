using System;
using System.Security;
using System.Text;

namespace PublicationAssistantSystem.Core
{
    public class Test
    {
        public static string UserName = "kneefer@gmail.com";
        public static string Password = "P@ssw0rd";
        public static string Cred { get; private set; }

        static Test()
        {
            Cred = ConvertCredentialsToBasicAccessFormat(UserName, Password);
        }

        public void Run()
        {
            var x = new Client
            {
                UseDefaultCredentials = false,
            };
            
            x.authenticate();
        }

        public static string ConvertCredentialsToBasicAccessFormat(string userName, string password)
        {
            var credentials = userName + ":" + password;
            var convertedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            return "Basic " + convertedCredentials;
        }

        public static byte[] ToByteArray(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
