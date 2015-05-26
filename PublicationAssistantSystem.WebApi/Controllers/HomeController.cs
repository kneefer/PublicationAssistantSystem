using System.Web.Mvc;

namespace PublicationAssistantSystem.WebApi.Controllers
{
    /// <summary>
    /// Main page controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Returns home page view.
        /// </summary>
        /// <returns> Home page view. </returns>
        public ActionResult Index()
        {
            ViewBag.Title = "PAS - Home";
            return View();
        }
    }
}