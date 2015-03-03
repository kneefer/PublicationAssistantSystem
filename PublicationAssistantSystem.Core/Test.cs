using System;
using System.Security;

namespace PublicationAssistantSystem.Core
{
    public class Test
    {
        public const string UserName = "kneefer@gmail.com";
        public const string Password = "P@ssw0rd";
        public const string CredentialsBased = "Basic a25lZWZlckBnbWFpbC5jb206UEBzc3cwcmQ=";


        public void Run()
        {
            var x = new Client
            {
                UseDefaultCredentials = false,
            };

            x.authenticate();
        }

        private static SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            unsafe
            {
                fixed (char* passwordChars = password)
                {
                    var securePassword = new SecureString(passwordChars, password.Length);
                    securePassword.MakeReadOnly();
                    return securePassword;
                }
            }
        }
    }
}
