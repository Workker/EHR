using EHR.Controller;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Util;
using EHR.UI.Filters;
using EHR.UI.Mappers;
using EHR.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class PatientController : System.Web.Mvc.Controller
    {
        #region Views

        public ActionResult Index(string cpf, string treatment)
        {
            var patient = FactoryController.GetController(ControllerEnum.Patient).GetBy(cpf);
            var patientModel = PatientMapper.MapPatientModelFrom(patient, treatment);
            var summary = FactoryController.GetController(ControllerEnum.Patient).GetSummaryBy(patient, treatment, GetAccount().Id);

            RegisterView(summary);

            var summaryModel = SummaryMapper.MapSummaryModelFrom(summary);

            summaryModel.Patient = patientModel;
            Session["Summary"] = summaryModel;

            if (GetSummary() == null)
                Response.Redirect("/Home");

            ViewBag.LastVisitors = summaryModel.LastVisitors != null
                                       ? DistinctView(summaryModel.LastVisitors
                                       .OrderByDescending(model => model.Id).Take(10).ToList())
                                       : new List<ViewModel>();
            ViewBag.Allergies = summaryModel.Allergies;
            ViewBag.Diagnostics = summaryModel.Diagnostics;
            ViewBag.Procedures = summaryModel.Procedures;
            ViewBag.Medications = summaryModel.Medications;

            return View(summaryModel);
        }

        #endregion

        #region PartialViews

        public PartialViewResult Form()
        {
            return PartialView("_Form");
        }

        public PartialViewResult Images()
        {
            return PartialView("_Images");
        }

        public PartialViewResult Prescriptions()
        {
            var medications = GetSummary().Medications;
            ViewBag.Medications = medications;
            return PartialView("_Prescriptions");
        }

        #endregion

        #region General Data

        public PartialViewResult GeneralData()
        {
            var summary = GetSummary();
            ViewBag.Allergies = summary.Allergies;
            ViewBag.Diagnostics = summary.Diagnostics;
            ViewBag.Medications = summary.Medications;
            return PartialView("_GeneralData", summary);
        }

        public string Admission(string q)
        {
            var stringReturn = "[";

            foreach (var id in Enum.GetValues(typeof(ReasonOfAdmissionEnum)).Cast<short>().ToList())
            {
                var description =
                    EnumUtil.GetDescriptionFromEnumValue(
                        (ReasonOfAdmissionEnum)Enum.Parse(typeof(ReasonOfAdmissionEnum), id.ToString()));
                if (description.Contains(q))
                {
                    stringReturn += "{\"name\":\"" + description + "\",\"id\":\"" + id + "\"}, ";
                }
            }
            stringReturn = stringReturn.Remove(stringReturn.Length - 2);
            stringReturn += "]";
            return stringReturn;
        }

        [HttpPost]
        public void SaveObservation(string observation)
        {
            FactoryController.GetController(ControllerEnum.Summary).SaveObservation(GetSummary().Id, observation);
            RefreshSessionSummary();
        }

        #region Allergy

        public PartialViewResult AllergyForm()
        {
            return PartialView("GeneralData/_AllergyForm");
        }

        public PartialViewResult SaveAllergy(string theWitch, List<string> type)
        {
            FactoryController.GetController(ControllerEnum.Allergy).SaveAllergy(theWitch, type.ConvertAll(short.Parse),
                                                                                GetSummary().Id);

            RefreshSessionSummary();
            ViewBag.Allergies = new List<AllergyModel> { GetSummary().Allergies.Last() };

            return PartialView("GeneralData/_AllergyTableRow");
        }

        public void DeleteAllergy(string id)
        {
            FactoryController.GetController(ControllerEnum.Allergy).RemoveAllergy(GetSummary().Id, int.Parse(id));
        }

        #endregion

        #region Diagnostic

        public PartialViewResult DiagnosticForm()
        {
            return PartialView("GeneralData/_DiagnosticsForm");
        }

        public PartialViewResult SaveDiagnostic(string type, string cidCode, string description)
        {
            FactoryController.GetController(ControllerEnum.Diagnostic).SaveDiagnostic(type, cidCode, GetSummary().Id);

            RefreshSessionSummary();
            ViewBag.Diagnostics = new List<DiagnosticModel> { GetSummary().Diagnostics.Last() };

            return PartialView("GeneralData/_DiagnosticTableRow");
        }

        public void DeleteDiagnostic(string id)
        {
            FactoryController.GetController(ControllerEnum.Diagnostic).RemoveDiagnostic(GetSummary().Id, int.Parse(id));
        }

        public JsonResult CidAutoComplete(string term)
        {
            var cids = FactoryController.GetController(ControllerEnum.Diagnostic).GetCids(term);

            return Json(cids, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Medication

        public PartialViewResult MedicationForm(string medicationType)
        {
            ViewBag.MedicationType = medicationType;
            return PartialView("Medication/_Form");
        }

        [HttpPost]
        public PartialViewResult SaveMedication(MedicationModel medication)
        {

            FactoryController.GetController(ControllerEnum.Summary).SaveMedication(GetSummary().Id, medication.Type,
                medication.Def.Id, medication.Presentation, medication.PresentationType, medication.Dose, medication.Dosage,
                medication.Way, medication.Place, medication.Frequency, medication.FrequencyCase, medication.Duration);

            RefreshSessionSummary();

            ViewBag.Medications = new List<MedicationModel> { GetSummary().Medications.Last() };
            ViewBag.MedicationType = medication.Type;
            return PartialView("Medication/_TableRow");
        }

        public void DeleteMedication(Medication medication)
        {
            FactoryController.GetController(ControllerEnum.Summary).RemoveMedication(GetSummary().Id, medication.Id);
            RefreshSessionSummary();
        }

        public JsonResult DefAutoComplete(string term)
        {
            var defDtOs = FactoryController.GetController(ControllerEnum.Def).GetDef(term);

            var defModels = DefMapper.MapDefModelsFrom(defDtOs);

            return Json(defModels, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Procedure

        public PartialViewResult Procedures()
        {
            SummaryModel summary = GetSummary();
            ViewBag.Procedures = summary.Procedures;
            return PartialView("_Procedures");
        }

        public PartialViewResult ProcedureForm()
        {
            return PartialView("Procedure/_ProcedureForm");
        }

        public PartialViewResult SaveProcedure(string day, string month, string year, string procedureCode,
                                               string procedure)
        {
            FactoryController.GetController(ControllerEnum.Procedure).SaveProcedure(day, month, year,
                                                                                    procedureCode, GetSummary().Id);

            RefreshSessionSummary();
            ViewBag.Procedures = new List<ProcedureModel> { GetSummary().Procedures.Last() };

            return PartialView("Procedure/_ProcedureTableRow");
        }

        public void DeleteProcedure(int id)
        {
            FactoryController.GetController(ControllerEnum.Procedure).RemoveProcedure(GetSummary().Id, id);
        }

        public JsonResult TusAutoComplete(string term)
        {
            List<TusDTO> tus = FactoryController.GetController(ControllerEnum.Procedure).GetTus(term);

            return Json(tus, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Exams

        public PartialViewResult Exams()
        {
            ViewBag.Exams = GetSummary().Exams;
            return PartialView("_Exams");
        }

        public PartialViewResult ExamForm()
        {
            return PartialView("Exams/_ExamsForm");
        }

        public PartialViewResult SaveExam(string type, string day, string month, string year, string description)
        {
            FactoryController.GetController(ControllerEnum.Summary).SaveExam(GetSummary().Id, short.Parse(type), day, month, year, description);

            RefreshSessionSummary();
            ViewBag.Exams = new List<ExamModel> { GetSummary().Exams.Last() };
            return PartialView("Exams/_ExamsTableRow");
        }

        public void DeleteExam(int id)
        {
            FactoryController.GetController(ControllerEnum.Summary).RemoveExam(GetSummary().Id, id);

            RefreshSessionSummary();
        }

        #endregion

        #region Hemotransfusion

        public PartialViewResult Hemotransfusion()
        {
            ViewBag.Hemotransfusions = GetSummary().Hemotransfusions;
            return PartialView("_hemotransfusion");
        }

        public PartialViewResult HemotransfusionForm()
        {
            return PartialView("Hemotransfusion/_HemotransfusionForm");
        }

        public PartialViewResult SaveHemotransfusion(List<string> typeReaction, string typeHemotrasfusion)
        {
            FactoryController.GetController(ControllerEnum.Hemotransfusion).SaveHemotransfusion(typeReaction,
                                                                                                typeHemotrasfusion,
                                                                                                GetSummary().Id);
            RefreshSessionSummary();
            ViewBag.Hemotransfusions = new List<HemotransfusionModel> { GetSummary().Hemotransfusions.Last() };

            return PartialView("Hemotransfusion/_HemotransfusionTableRow");
        }

        public void DeleteHemotransfusion(string id)
        {
            FactoryController.GetController(ControllerEnum.Hemotransfusion).RemoveHemotransfusion(GetSummary().Id,
                                                                                                  int.Parse(id));
        }

        #endregion

        #region MDR

        public PartialViewResult ColonizationbyMdr()
        {
            ViewBag.Mdr = GetSummary().Mdr;
            return PartialView("_ColonizationbyMDR");
        }

        [HttpPost]
        public void SaveMdr(string mdr)
        {
            FactoryController.GetController(ControllerEnum.Summary).SaveMdr(GetSummary().Id, mdr);
            RefreshSessionSummary();
        }

        #endregion

        #region High Data

        public PartialViewResult DataHigh()
        {
            var summary = GetSummary();
            Session["ComplementaryExams"] = summary.HighData.ComplementaryExams;
            ViewBag.ComplementaryExams = GetComplementaryExamsFromSession();
            return PartialView("_DataHigh", summary);
        }

        [HttpPost]
        public void SaveHighData(HighDataModel highDataModel)
        {
            var complementaryExam = new List<ComplementaryExam>();
            var complementaryExamDeleteds = new List<int>();

            foreach (var complementaryExamModel in GetComplementaryExamsFromSession())
            {
                if (complementaryExamModel.Id == 0)
                {
                    complementaryExam.Add(new ComplementaryExam()
                    {
                        Description = complementaryExamModel.Description,
                        Period = complementaryExamModel.Period
                    });
                }

                if (complementaryExamModel.Deleted)
                {
                    complementaryExamDeleteds.Add(complementaryExamModel.Id);
                }
            }

            FactoryController.GetController(ControllerEnum.Summary).SaveHighData
                (
                GetSummary().Id,
                complementaryExam,
                complementaryExamDeleteds,
                highDataModel.HighType,
                highDataModel.ConditionOfThePatientAtHigh,
                highDataModel.DestinationOfThePatientAtDischarge,
                highDataModel.OrientationOfMultidisciplinaryTeamsMet,
                highDataModel.TermMedicalReviewAt, highDataModel.Specialty.Id,
                new DateTime(highDataModel.PrescribedHighYear, highDataModel.PrescribedHighMonth, highDataModel.PrescribedHighDay),
                highDataModel.PersonWhoDeliveredTheSummary,
                new DateTime(highDataModel.DeliveredDateYear, highDataModel.DeliveredDateMonth, highDataModel.DeliveredDateDay)
                    );

            RefreshSessionSummary();
        }

        public JsonResult SpecialtyAutoComplete(string term)
        {
            var specialties = FactoryController.GetController(ControllerEnum.Specialty).GetSpecialty(term);

            var specialtyModels = specialties != null ? SpecialtyMapper.MapSpecialtyModelsFrom(specialties) : new List<SpecialtyModel>();

            return Json(specialtyModels, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ComplementaryExamForm()
        {
            return PartialView("DataHigh/_ComplementaryExamForm");
        }

        [HttpPost]
        public PartialViewResult SaveComplementaryExam(ComplementaryExamModel complementaryExamModel)
        {
            AddOnSessionComplementaryExams(complementaryExamModel);

            ViewBag.ComplementaryExams = new List<ComplementaryExamModel> { GetComplementaryExamsFromSession().Last() };
            return PartialView("DataHigh/_ComplementaryExamTableRow");
        }

        private void AddOnSessionComplementaryExams(ComplementaryExamModel complementaryExamModel)
        {
            var complementaryExams = GetComplementaryExamsFromSession();
            complementaryExams.Add(complementaryExamModel);
            Session["ComplementaryExams"] = complementaryExams;
        }

        private List<ComplementaryExamModel> GetComplementaryExamsFromSession()
        {
            return (List<ComplementaryExamModel>)Session["ComplementaryExams"];
        }

        [HttpPost]
        public void DeleteComplementaryExam(int id)
        {
            if (id != 0)
            {
                var complementaryExam = GetComplementaryExamsFromSession();

                complementaryExam.Find(c => c.Id == id).Deleted = true;

                Session["ComplementaryExams"] = complementaryExam;
            }
        }

        #endregion

        #region Private Methods

        private SummaryModel GetSummary()
        {
            return (SummaryModel)Session["Summary"];
        }

        private void RegisterView(Summary summary)
        {
            FactoryController.GetController(ControllerEnum.Summary).AddView(summary.Id,
                                                                               ((AccountModel)Session["account"]).Id, DateTime.Now);
        }

        private AccountModel GetAccount()
        {
            return (AccountModel)Session["account"];
        }

        private void RefreshSessionSummary()
        {
            var summary = FactoryController.GetController(ControllerEnum.Summary).GetBy(GetSummary().Id);
            Session["Summary"] = SummaryMapper.MapSummaryModelFrom(summary);
        }

        private IList<ViewModel> DistinctView(IList<ViewModel> viewModels)
        {
            return (IList<ViewModel>)viewModels.GroupBy(x => x.Account.Id).Select(x => x.FirstOrDefault()).ToList();
        }

        #endregion
    }
}