using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View(Session["account"]);
        }
    }
}
