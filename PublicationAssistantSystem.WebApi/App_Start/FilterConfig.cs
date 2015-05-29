using System.Web.Mvc;

namespace PublicationAssistantSystem.WebApi
{
    /// <summary>
    /// Filter configuration.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers filters collection.
        /// </summary>
        /// <param name="filters"> Filter collection. </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
