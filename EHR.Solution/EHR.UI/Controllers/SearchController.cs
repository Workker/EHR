using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHRIntegracao.Domain.Factorys;

namespace EHR.UI.Controllers
{
    public class SearchController : System.Web.Mvc.Controller
    {
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult Index(string query)
        {
            Session["Date"] = null;
            var patient = new Domain.PatientDTO { Name = query };
            var patientController = new EHR.Controller.PatientController();
            ViewBag.Patients = patientController.GetBy(patient,new List<string>()).Take(30);
            Session["Name"] = query;
            return PartialView("_Search");
        }
        [HttpPost]
        public ActionResult FilterPeople(string dob_day, string dob_month, string dob_year, List<string> hospital)
        {
            Session["Hospital"] = hospital ?? (hospital =  new List<string>());
            Session["Date"] = dob_day + "/" + dob_month + "/" + dob_year;
            var patient = new Domain.PatientDTO { Name = Session["Name"].ToString(), DateBirthday = Session["Date"].ToString() };
            var patientController = new EHR.Controller.PatientController();
            ViewBag.Patients = patientController.GetBy(patient,hospital);
            return PartialView("Layout/_Result");
        }

        public ActionResult More()
        {
            if (Session["Hospital"] == null)
                Session["Hospital"] = new List<string>();

            ManagerSession();
            var patient = new Domain.PatientDTO { Name = Session["Name"].ToString() };

            if (Session["Date"] != null && !string.IsNullOrEmpty(Session["Date"].ToString()))
                patient.DateBirthday = Session["Date"].ToString();

            var patientController = new EHR.Controller.PatientController();
            ViewBag.Patients = patientController.GetBy(patient, (List<string>)Session["Hospital"]).Skip((int)Session["Skip"]).Take((int)Session["Take"]);

            return PartialView("Layout/_Result");
        }

        private void ManagerSession()
        {
            Session["Skip"] = 10 + (Session["Skip"] != null ? (int) Session["Skip"] : 0);
            Session["Take"] = 10 + (Session["Take"] != null ? (int) Session["Take"] : 0);
        }
    }
}
