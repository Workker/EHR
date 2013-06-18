using AutoMapper;
using EHR.Controller;
using EHR.Domain.Entities;
using EHR.Domain.Util;
using EHR.UI.Filters;
using EHR.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EHR.CoreShared;


namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class PatientController : System.Web.Mvc.Controller
    {
        #region Views

        public ActionResult Index(string cpf, string treatment)
        {
            var patient = FactoryController.GetController(ControllerEnum.Patient).GetBy(cpf);
            var patientModel = MapPatientModelFrom(patient, treatment);
            var summary = FactoryController.GetController(ControllerEnum.Patient).GetSummaryBy(patient, treatment, GetAccount().Id);
            var summaryModel = MapSummaryModelFrom(summary);

            summaryModel.Patient = patientModel;
            Session["Summary"] = summaryModel;

            if (GetSummary() == null)
                Response.Redirect("/Home");

            ViewBag.Allergies = summaryModel.Allergies;
            ViewBag.Diagnostics = summaryModel.Diagnostics;
            ViewBag.Procedures = summaryModel.Procedures;

            return View(summaryModel);
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

        #endregion

        #region General Data

        public PartialViewResult GeneralData()
        {
            return PartialView("_GeneralData", GetSummary());
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

        #region medicament of previous use

        public PartialViewResult MedicamentOfPreviousUseForm()
        {
            return PartialView("GeneralData/_MedicamentOfPreviousUseForm");
        }

        public PartialViewResult SaveMedicamentOfPreviousUse()
        {
            return PartialView("GeneralData/_MedicamentOfPreviousUseTableRow");
        }

        public void DeleteMedicamentOfPreviousUse()
        {
        }

        #endregion

        #region Medicament used during hospitalization

        public PartialViewResult MedicamentUsedDuringhospitalizationForm()
        {
            return PartialView("GeneralData/_MedicamentUsedDuringhospitalizationForm");
        }

        public PartialViewResult SaveMedicamentUsedDuringhospitalization()
        {
            return PartialView("GeneralData/_MedicamentUsedDuringhospitalizationTableRow");
        }

        public void DeleteMedicamentUsedDuringhospitalization()
        {
        }

        #endregion

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

        public PartialViewResult SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode,
                                               string procedure)
        {
            FactoryController.GetController(ControllerEnum.Procedure).SaveProcedure(dob_day, dob_month, dob_year,
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

        #region Hemotransfusion

        public PartialViewResult Hemotransfusion()
        {
            return PartialView("_hemotransfusion", GetSummary());
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

        public void SaveMdr(string mdr)
        {
            FactoryController.GetController(ControllerEnum.Summary).SaveMdr(GetSummary().Id, mdr);
        }

        #endregion

        #region Private Methods

        private SummaryModel GetSummary()
        {
            return (SummaryModel)Session["Summary"];
        }

        private AccountModel GetAccount()
        {
            return (AccountModel)Session["account"];
        }

        private void RefreshSessionSummary()
        {
            var summary = FactoryController.GetController(ControllerEnum.Summary).GetBy(GetSummary().Id);
            Session["Summary"] = MapSummaryModelFrom(summary);
        }

        private static void AddHospital(IPatientDTO patient, string treatmentStr, PatientModel patientModel)
        {
            if (patient.Treatments != null && patient.Treatments.Count > 0 && !string.IsNullOrEmpty(treatmentStr) &&
                patient.Treatments.Count(t => t.Id == treatmentStr) > 0)
            {
                patientModel.Hospital =
                    EnumUtil.GetDescriptionFromEnumValue(
                        (DbEnum)
                        Enum.Parse(typeof(DbEnum),
                                   patient.Treatments.FirstOrDefault(t => t.Id == treatmentStr).Hospital.ToString()));
            }
            else
                patientModel.Hospital =
                    EnumUtil.GetDescriptionFromEnumValue((DbEnum)Enum.Parse(typeof(DbEnum), patient.Hospital.ToString()));
        }

        #region Mappings

        private static PatientModel MapPatientModelFrom(IPatientDTO patient, string treatmentStr)
        {
            Mapper.CreateMap<IPatientDTO, PatientModel>().ForMember(dest => dest.Treatments, source => source.Ignore());

            var patientModel = Mapper.Map<IPatientDTO, PatientModel>(patient);
            var treatmentModels = new List<TreatmentModel>();

            AddHospital(patient, treatmentStr, patientModel);

            foreach (var treatment in patient.Treatments)
            {
                var treatmentModel = MapTreatmentModelFrom(treatment);
                treatmentModels.Add(treatmentModel);
            }

            patientModel.Treatments = treatmentModels;

            return patientModel;
        }

        private static TreatmentModel MapTreatmentModelFrom(ITreatmentDTO treatment)
        {
            Mapper.CreateMap<ITreatmentDTO, TreatmentModel>();
            return Mapper.Map<ITreatmentDTO, TreatmentModel>(treatment);
        }

        private static SummaryModel MapSummaryModelFrom(Summary summary)
        {
            Mapper.CreateMap<Summary, SummaryModel>().ForMember(dest => dest.Hospital, source => source.Ignore()).ForMember(al => al.Allergies, so => so.Ignore());
            var summaryModel = Mapper.Map<Summary, SummaryModel>(summary);

            summaryModel.Allergies = MapAllergyModelsFrom(summary.Allergies);
            summaryModel.Diagnostics = MapDiagnosticsModelsFrom(summary.Diagnostics);
            summaryModel.Procedures = MapProceduresModelsFrom(summary.Procedures);
            summaryModel.Hemotransfusions = MapHemotransfusionModelFrom(summary.Hemotransfusions);

            return summaryModel;
        }

        private static List<HemotransfusionModel> MapHemotransfusionModelFrom(IList<Hemotransfusion> hemotransfusions)
        {
            var hemoModels = new List<HemotransfusionModel>();
            foreach (var hemotransfusion in hemotransfusions)
            {
                Mapper.CreateMap<Hemotransfusion, HemotransfusionModel>();
                var hemotransfusionModel = Mapper.Map<Hemotransfusion, HemotransfusionModel>(hemotransfusion);
                foreach (var reaction in hemotransfusion.Reactions)
                {
                    hemotransfusionModel.ReactionType.Add(reaction.Id);
                }

                hemoModels.Add(hemotransfusionModel);
            }
            return hemoModels;
        }

        private static List<ProcedureModel> MapProceduresModelsFrom(IList<Procedure> procedures)
        {
            var proceduresModels = new List<ProcedureModel>();
            foreach (var procedure in procedures)
            {
                Mapper.CreateMap<Procedure, ProcedureModel>();
                var procedureModel = Mapper.Map<Procedure, ProcedureModel>(procedure);
                proceduresModels.Add(procedureModel);
            }
            return proceduresModels;
        }

        private static List<DiagnosticModel> MapDiagnosticsModelsFrom(IList<Diagnostic> diagnostics)
        {
            var diagnosticsModels = new List<DiagnosticModel>();
            foreach (var diagnostic in diagnostics)
            {
                Mapper.CreateMap<Diagnostic, DiagnosticModel>().ForMember(cid => cid, source => source.Ignore());
                var diagnosticsModel = Mapper.Map<Diagnostic, DiagnosticModel>(diagnostic);
                diagnosticsModel.Cid = MapCidModelFrom(diagnostic.Cid);
                diagnosticsModels.Add(diagnosticsModel);
            }
            return diagnosticsModels;
        }

        private static List<AllergyModel> MapAllergyModelsFrom(IList<Allergy> allergies)
        {
            var allergyModels = new List<AllergyModel>();

            foreach (var allergy in allergies)
            {
                Mapper.CreateMap<Allergy, AllergyModel>().ForMember(type => type.Types, source => source.Ignore());
                var allergyModel = Mapper.Map<Allergy, AllergyModel>(allergy);

                foreach (var allergyType in allergy.Types)
                {
                    allergyModel.Types.Add(allergyType.Id);
                }
                allergyModels.Add(allergyModel);
            }
            return allergyModels;
        }

        private static CidModel MapCidModelFrom(Cid cid)
        {
            Mapper.CreateMap<Cid, CidModel>();
            return Mapper.Map<Cid, CidModel>(cid);
        }

        #endregion

        #endregion
    }
}