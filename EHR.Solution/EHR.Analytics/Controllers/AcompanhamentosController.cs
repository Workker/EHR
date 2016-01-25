using EHR.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHR.Analytics.Controllers
{
    public class AcompanhamentosController : System.Web.Mvc.Controller
    {
        private PrescriptionsController _controller = new PrescriptionsController();

        // GET: Acompanhamentos
        public ActionResult Index()
        {
            ViewBag.Prescricoes = _controller.All();
            return View();
        }

        public JsonResult getByPeriod(DateTime? initialPeriod, DateTime? closePeriod)
        {
            return Json(_controller.getByPeriod(initialPeriod, closePeriod), JsonRequestBehavior.AllowGet);
        }
    }
}