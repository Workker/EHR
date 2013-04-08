﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHRIntegracao.Domain.Factorys;
using EHRIntegracao.Domain.Services.DTO;


namespace EHR.UI.Controllers
{
    public class PatientController : System.Web.Mvc.Controller
    {
        #region Views

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region PartialViews

        public PartialViewResult Antimicrobial()
        {
            return PartialView("_Antimicrobial");
        }

        public PartialViewResult Charts()
        {
            return PartialView("_Charts");
        }

        public PartialViewResult ColonizationbyMdr()
        {
            return PartialView("_ColonizationbyMDR");
        }

        public PartialViewResult DataHigh()
        {
            return PartialView("_DataHigh");
        }

        public PartialViewResult Exams()
        {
            return PartialView("_Exams");
        }

        public PartialViewResult Form()
        {
            return PartialView("_Form");
        }

        public PartialViewResult GeneralData()
        {
            return PartialView("_GeneralData");
        }

        public PartialViewResult Hemotransfusion()
        {
            return PartialView("_hemotransfusion");
        }

        public PartialViewResult Images()
        {
            return PartialView("_Images");
        }

        public PartialViewResult OtherMedicationsRelevant()
        {
            return PartialView("_OtherMedicationsRelevant");
        }

        public PartialViewResult Prescriptions()
        {
            return PartialView("_Prescriptions");
        }

        public PartialViewResult Procedures()
        {
            return PartialView("_Procedures");
        }

        #endregion

        public string Admission(string q)
        {
            string stringReturn = null;

            if (q.Equals("c") || q.Equals("C"))
            {
                stringReturn = "[{\"name\":\"Clínica\",\"id\":\"1\"}, {\"name\":\"Cirúrgica\",\"id\":\"2\"}]";
            }
            else if (q.Equals("e") || q.Equals("E"))
            {
                stringReturn = "[{\"name\":\"Eletiva\",\"id\":\"3\"}, {\"name\":\"Emergência\",\"id\":\"4\"}]";
            }
            return stringReturn;
        }

        public string SearchPeaple(string query)
        {
            var patient = new Domain.PatientDTO { Name = query };
            var patientController = new EHR.Controller.PatientController();

            return BuildResultsOfSimpleSearchOfPatients(patientController.GetBy(DbEnum.QuintaDorWorkker, patient));
        }

        #region Diagnostic

        public PartialViewResult DiagnosticForm()
        {
            return PartialView("GeneralData/_DiagnosticsForm");
        }

        public PartialViewResult SaveDiagnostic(string type, string code, string description)
        {
            return PartialView("GeneralData/_DiagnosticTableRow");
        }

        public void DeleteDiagnostic()
        {
        }

        #endregion

        #region Exams

        public PartialViewResult ExamForm()
        {
            return PartialView("Exams/_ExamsForm");
        }

        public PartialViewResult SaveExam(string type, string code, string description)
        {
            return PartialView("Exams/_ExamsTableRow");
        }

        public void DeleteExam()
        {

        }

        #endregion

        #region Allergy


        public PartialViewResult AllergyForm()
        {
            return PartialView("GeneralData/_AllergyForm");
        }

        public PartialViewResult SaveAllergy(string type, string code, string description)
        {
            return PartialView("GeneralData/_AllergyTableRow");
        }

        public void DeleteAllergy()
        {
        }

        #endregion

        #region medicament of previous use


        public PartialViewResult MedicamentOfPreviousUseForm()
        {
            return PartialView("GeneralData/_MedicamentOfPreviousUseForm");
        }

        public PartialViewResult SaveMedicamentOfPreviousUse(string type, string code, string description)
        {
            return PartialView("GeneralData/_MedicamentOfPreviousUseTableRow");
        }

        public void DeleteMedicamentOfPreviousUse()
        {
        }

        #endregion

        #region medicament used during hospitalization


        public PartialViewResult MedicamentUsedDuringhospitalizationForm()
        {
            return PartialView("GeneralData/_MedicamentUsedDuringhospitalizationForm");
        }

        public PartialViewResult SaveMedicamentUsedDuringhospitalization(string type, string code, string description)
        {
            return PartialView("GeneralData/_MedicamentUsedDuringhospitalizationTableRow");
        }

        public void DeleteMedicamentUsedDuringhospitalization()
        {
        }

        #endregion


        private string BuildResultsOfSimpleSearchOfPatients(IEnumerable<IPatientDTO> patients)
        {
            var result = patients.Aggregate("{\"results\":[{\"type\":\"header\",\"text\":\"Pacientes\"}", (current, patient) => current + (",{\"type\":\"person\",\"name\":\"" + patient.Name + "\",\"hospital\":\"" +Enum.GetName(typeof(DbEnum) ,patient.Hospital) + "\", \"imageUrl\":\"../Images/Profiles/1.jpg\"}"));

            return result += "]}";

        }

    }
}
