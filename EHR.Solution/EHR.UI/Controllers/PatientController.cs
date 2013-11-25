using EHR.UI.Filters;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class PatientController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}