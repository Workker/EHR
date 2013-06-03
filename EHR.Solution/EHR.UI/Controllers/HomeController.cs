using System.Web.Mvc;
using EHR.UI.Filters;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class HomeController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View(Session["account"]);
        }
    }
}
