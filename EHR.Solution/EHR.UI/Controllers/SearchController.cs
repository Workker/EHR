using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHR.Domain;
using EHRIntegracao.Domain.Factorys;
using EHRIntegracao.Domain.Services.DTO;

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
            var patient = new PatientDTO { Name = query };
            var patientController = new EHR.Controller.PatientController();
            ViewBag.Patients = patientController.GetBy(patient, new List<string>()).Take(10);
            Session["Name"] = query;
            return PartialView("_Search");
        }
        [HttpPost]
        public ActionResult FilterPeople(string dob_day, string dob_month, string dob_year, List<string> hospital)
        {
            Session["Hospital"] = hospital;
            FillDateParameter(dob_day, dob_month, dob_year);
            ViewBag.Patients = GetTreatment(false);
            return PartialView("Layout/_Result");
        }

        public ActionResult More()
        {
            ViewBag.Patients = GetTreatment(true);
            return PartialView("Layout/_Result");
        }

        private IEnumerable<IPatientDTO> GetTreatment(bool skip)
        {
            ManagerSession(skip);
            var patient = PreencherPaciente();
            return ObterPacientes(patient,skip);
        }

        private void FillDateParameter(string dob_day, string dob_month, string dob_year)
        {
            Session["Date"] = dob_day + "/" + dob_month + "/" + dob_year;
        }

        private PatientDTO PreencherPaciente()
        {
            var patient = new PatientDTO { Name = Session["Name"].ToString() };

            if (Session["Date"] != null && !string.IsNullOrEmpty(Session["Date"].ToString()))
                patient.DateBirthday =(DateTime?) Session["Date"];
            return patient;
        }

        private IEnumerable<IPatientDTO> ObterPacientes(PatientDTO patient, bool skip)
        {
            var patientController = new EHR.Controller.PatientController();

            if (skip)
                return patientController.GetBy(patient, (List<string>)Session["Hospital"]).Skip((int)Session["Skip"]).Take(10);
            else
                return patientController.GetBy(patient, (List<string>)Session["Hospital"]).Take(10);
        }

        private void ManagerSession(bool skip)
        {
            if (Session["Hospital"] == null)
                Session["Hospital"] = new List<string>();


            if (skip)
                Session["Skip"] = 10 + (Session["Skip"] != null ? (int)Session["Skip"] : 0);
            else
                Session["Skip"] = 0;
        }
    }
}
