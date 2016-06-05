using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Form()
        {
            return PartialView();
        }
     }
}