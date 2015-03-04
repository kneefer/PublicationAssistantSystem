using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PublicationAssistantSystem.Web.Startup))]
namespace PublicationAssistantSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
