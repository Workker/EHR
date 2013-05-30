using System.Web.Mvc;
using EHR.Controller;
using EHR.UI.Models;

namespace EHR.UI.Controllers
{
    public class AccountController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(DoctorModel doctor)
        {
            FactoryController.GetController(ControllerEnum.Account).Register();
            return null;
        }

    }
}
