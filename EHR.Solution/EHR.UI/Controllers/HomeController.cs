using System.Web.Mvc;
using EHR.UI.Filters;

namespace EHR.UI.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {
        [AuthenticationFilter]
        public ActionResult Index()
        {
            return View(Session["account"]);
        }
    }
}
