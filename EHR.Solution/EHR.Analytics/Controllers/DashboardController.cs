using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHR.Analytics.Controllers
{
    public class DashboardController : System.Web.Mvc.Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.NomePagina = "DashBoard";
            return View();
        }
    }
}