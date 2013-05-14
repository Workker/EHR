using System.Linq;
using EHR.Controller;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace EHR.UI.Controllers
{
    public class PatientController : System.Web.Mvc.Controller
    {
        public EHR.Controller.ProcedureController ProcedureController
        {
            get { return new EHR.Controller.ProcedureController(); }
        }

        #region Views
        public ActionResult Index(string cpf)
        {
            var controller = new EHR.Controller.PatientController();
            var patient = controller.GetBy(cpf);
            ViewBag.data = patient;
            ViewBag.age = CalculateAgeFrom((DateTime)patient.DateBirthday);
            Session["Summary"] = controller.GetSummaryByPatient(patient);

            if (Session["Summary"] == null)
                Response.Redirect("/Home");


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
            Summary summary = GetSummary();
            ViewBag.Procedures = Convert(summary.Procedures);
            return PartialView("_Procedures");
        }

        public List<ProcedureModel> Convert(IList<Procedure> procedures)
        {
            var proceduresModels = new List<ProcedureModel>();

            foreach (var procedure in procedures)
            {
                proceduresModels.Add(new ProcedureModel()
                                         {
                                             Code = procedure.GetCode()
                                             ,
                                             Description = procedure.GetDescription()
                                             ,
                                             Date = procedure.Date
                                             ,
                                             Id = procedure.Id
                                         });
            }
            return proceduresModels;
        }

        public List<ProcedureModel> ConvertLast(IList<Procedure> procedures)
        {
            var proceduresModels = new List<ProcedureModel>();

                proceduresModels.Add(new ProcedureModel()
                {
                    Code = procedures.Last().GetCode()
                    ,
                    Description = procedures.Last().GetDescription()
                    ,
                    Date = procedures.Last().Date
                    ,
                    Id = procedures.Last().Id
                });
            
            return proceduresModels;
        }

        public Summary GetSummary()
        {
            return (Summary)Session["Summary"];
        }

        #endregion

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

        public PartialViewResult SaveAllergy(string factor, List<string> type)
        {
            ViewBag.types = type;
            ViewBag.factor = factor;
            return PartialView("GeneralData/_AllergyTableRow");
        }

        public void DeleteAllergy()
        {
        }

        #endregion

        #region Procedure


        public PartialViewResult ProcedureForm()
        {
            return PartialView("Procedure/_ProcedureForm");
        }

        public PartialViewResult SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, string procedure)
        {
            FactoryController.GetController(ControllerEnum.Procedure).SaveProcedure(dob_day, dob_month, dob_year,
                                                                                    procedureCode, GetSummary());
            ViewBag.Procedures = ConvertLast(GetSummary().Procedures);
            return PartialView("Procedure/_ProcedureTableRow");
        }

        public void DeleteProcedure(int id)
        {
            var summary = GetSummary();

            FactoryController.GetController(ControllerEnum.Procedure).RemoveProcedure(summary, id);
        }

        #endregion

        #region Hemotransfusion


        public PartialViewResult HemotransfusionForm()
        {
            return PartialView("Hemotransfusion/_HemotransfusionForm");
        }

        public PartialViewResult SaveHemotransfusion(string dob_day, string dob_month, string dob_year, string procedureCode, string procedure)
        {
            ViewBag.procedure = procedureCode + " - " + procedure;
            ViewBag.data = dob_day + "/" + dob_month + "/" + dob_year;
            return PartialView("Hemotransfusion/_HemotransfusionTableRow");
        }

        public void DeleteHemotransfusion()
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

        #region private methods

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

        private int CalculateAgeFrom(DateTime birthday)
        {
            return DateTime.Today.Year - birthday.Year;
        }

        #endregion

        #region AutoComplet

        public JsonResult TusAutoComplete(string term)
        {
            List<Tus> tus = FactoryController.GetController(ControllerEnum.Procedure).GetTus();

            return Json(tus, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
