using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace PublicationAssistantSystem.WebApi
{
    /// <summary>
    /// Configures web api settings like routes and authentication.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers routes for web api.
        /// </summary>
        /// <param name="config"> Configuration. </param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Publications area route
            config.Routes.MapHttpRoute(
                "DefaultPublicationsArea",
                "api/Publications/{controller}/{id}",
                new { area = "Publications", id = RouteParameter.Optional }
                );

            // Default route
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
                );
        }
    }
}