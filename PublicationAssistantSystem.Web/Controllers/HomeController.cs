using System.Web.Mvc;
using PublicationAssistantSystem.DAL.Context;

namespace PublicationAssistantSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PublicationAssistantContext _context = new PublicationAssistantContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}