using EHR.Controller;
using EHR.CoreShared;
using EHR.CoreShared.Entities;
using EHR.CoreShared.Interfaces;
using EHR.UI.Filters;
using EHR.UI.Infrastructure.Notification;
using EHR.UI.Models;
using EHR.UI.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class SearchController : System.Web.Mvc.Controller
    {
        public ActionResult Index(string query)
        {
            try
            {
                Session["Date"] = null;
                var patient = new Patient { Name = query };
                var patientController = FactoryController.GetController(ControllerEnum.Patient);
                ViewBag.Patients = patientController.GetBy(patient, new List<short>()).Take(10);
                ViewBag.Hospitals =
                    HospitalMapper.MapHospitalModelFrom(
                        FactoryController.GetController(ControllerEnum.Hospital).GetAllHospitals());
                Session["Name"] = query;
                return PartialView("_Search");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        [HttpPost]
        public ActionResult FilterPeople(string day, string month, string year, List<short> hospitalId)
        {
            try
            {
                Session["Hospital"] = hospitalId ?? (hospitalId = new List<short>());
                FillDateParameter(day, month, year);
                ViewBag.Patients = GetTreatment(false);

                this.ShowMessage(MessageTypeEnum.Success, "Filtro alterado.");

                return PartialView("Layout/_Result");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        public ActionResult More()
        {
            try
            {
                ViewBag.Patients = GetTreatment(true);
                return PartialView("Layout/_Result");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        public string SearchPeaple(string query)
        {
            try
            {
                var patient = new Patient { Name = query };
                var patientController = FactoryController.GetController(ControllerEnum.Patient);
                var patients = PatientMapper.MapPatientModelFrom(patientController.GetBy(patient));
                return BuildResultsOfSimpleSearchOfPatients(patients);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        #region Private Methods

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
            var result = patients.Aggregate("{\"results\":[{\"type\":\"header\",\"text\":\"Pacientes\"}", (current, patient) => current + (",{\"type\":\"person\",\"cpf\":\"" + patient.Cpf + "\",\"name\":\"" + patient.Name + "\",\"hospital\":\"" + patient.Hospital.Name + "\", \"imageUrl\":\"/Images/Profiles/1.jpg\"}"));
            return result + "]}";
        }

        private IEnumerable<IPatient> GetTreatment(bool skip)
        {
            ManagerSession(skip);
            var patient = FillPatients();
            return GetPatients(patient, skip);
        }

        private Patient FillPatients()
        {
            var patient = new Patient { Name = Session["Name"].ToString() };

            if (Session["Date"] != null && !string.IsNullOrEmpty(Session["Date"].ToString()) && (string)Session["Date"] != "-1/-1/-1")
                patient.DateBirthday = Convert.ToDateTime(Session["Date"]);
            return patient;
        }

        private IEnumerable<IPatient> GetPatients(Patient patient, bool skip)
        {
            var patientController = FactoryController.GetController(ControllerEnum.Patient);

            return skip ? patientController.GetBy(patient, (List<short>)Session["Hospital"]).Skip((int)Session["Skip"]).Take(10) : patientController.GetBy(patient, (List<short>)Session["Hospital"]).Take(10);
        }

        private void FillDateParameter(string day, string month, string year)
        {
            Session["Date"] = day + "/" + month + "/" + year;
        }

        #endregion
    }
}
