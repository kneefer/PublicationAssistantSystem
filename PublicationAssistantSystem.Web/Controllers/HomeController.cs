using System.Web.Mvc;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;

namespace PublicationAssistantSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PublicationAssistantContext _context = new PublicationAssistantContext();

        public ActionResult Index()
        {
            _context.Faculties.Add(new Faculty());
            _context.SaveChanges();

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