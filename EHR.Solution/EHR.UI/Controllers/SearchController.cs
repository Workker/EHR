using AutoMapper;
using EHR.Controller;
using EHR.CoreShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EHR.Domain.Util;
using EHR.UI.Filters;
using EHR.UI.Models;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class SearchController : System.Web.Mvc.Controller
    {
        #region Views

        public ActionResult Index(string query)
        {
            Session["Date"] = null;
            var patient = new PatientDTO { Name = query };
            var patientController = FactoryController.GetController(ControllerEnum.Patient);
            ViewBag.Patients = patientController.GetBy(patient, new List<string>()).Take(10);
            Session["Name"] = query;
            return PartialView("_Search");
        }

        #endregion

        #region Ajax Methods

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

        public string SearchPeaple(string query)
        {
            var patient = new PatientDTO { Name = query };
            var patientController = FactoryController.GetController(ControllerEnum.Patient);
            var patients = MapPatientModelFrom(patientController.GetBy(patient));
            return BuildResultsOfSimpleSearchOfPatients(patients);
        }

        #endregion

        #region Private Methods

        private static IEnumerable<PatientModel> MapPatientModelFrom(IEnumerable<IPatientDTO> patients)
        {
            Mapper.CreateMap<IPatientDTO, PatientModel>().ForMember(dest => dest.Hospital, source => source.Ignore());

            var patientModels = new List<PatientModel>();

            foreach (var item in patients)
            {
                var account = Mapper.Map<IPatientDTO, PatientModel>(item);
                account.Hospital = EnumUtil.GetDescriptionFromEnumValue((DbEnum)Enum.Parse(typeof(DbEnum), item.Hospital.ToString()));
                patientModels.Add(account);
            }
            return patientModels;
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

        private string BuildResultsOfSimpleSearchOfPatients(IEnumerable<PatientModel> patients)
        {
            var result = patients.Aggregate("{\"results\":[{\"type\":\"header\",\"text\":\"Pacientes\"}", (current, patient) => current + (",{\"type\":\"person\",\"cpf\":\"" + patient.CPF + "\",\"name\":\"" + patient.Name + "\",\"hospital\":\"" + patient.Hospital + "\", \"imageUrl\":\"../Images/Profiles/1.jpg\"}"));
            return result += "]}";
        }

        private IEnumerable<IPatientDTO> GetTreatment(bool skip)
        {
            ManagerSession(skip);
            var patient = FillPatients();
            return GetPatients(patient, skip);
        }

        private PatientDTO FillPatients()
        {
            var patient = new PatientDTO { Name = Session["Name"].ToString() };

            if (Session["Date"] != null && !string.IsNullOrEmpty(Session["Date"].ToString()) && (string)Session["Date"] != "//")
                patient.DateBirthday = Convert.ToDateTime(Session["Date"]);
            return patient;
        }

        private IEnumerable<IPatientDTO> GetPatients(PatientDTO patient, bool skip)
        {
            var patientController = FactoryController.GetController(ControllerEnum.Patient);

            return skip ? patientController.GetBy(patient, (List<string>)Session["Hospital"]).Skip((int)Session["Skip"]).Take(10) : patientController.GetBy(patient, (List<string>)Session["Hospital"]).Take(10);
        }

        private void FillDateParameter(string dob_day, string dob_month, string dob_year)
        {
            Session["Date"] = dob_day + "/" + dob_month + "/" + dob_year;
        }

        #endregion
    }
}
