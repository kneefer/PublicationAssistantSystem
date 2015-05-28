using System.Web.Mvc;

namespace PublicationAssistantSystem.WebApi.Areas.Publications
{
    public class PublicationsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Publications";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Publications_default",
                "Publications/{controller}/{id}",
                new { id = UrlParameter.Optional }
            );
        }
    }
}