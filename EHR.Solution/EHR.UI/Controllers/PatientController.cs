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
            return PartialView("_GeneralData");
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

            var defModels = MapDefModelsFrom(defDtOs);

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
            Session["ComplementaryExams"] = new List<ComplementaryExamModel>();
            ViewBag.ComplementaryExams = GetComplementaryExamsFromSession();
            return PartialView("_DataHigh", GetSummary());
        }

        [HttpPost]
        public void SaveHighData(HighDataModel highDataModel)
        {
            FactoryController.GetController(ControllerEnum.Summary).SaveHighData
                (
                GetSummary().Id,
                null,
                highDataModel.HighType,
                highDataModel.ConditionOfThePatientAtHigh,
                highDataModel.DestinationOfThePatientAtDischarge,
                highDataModel.OrientationOfMultidisciplinaryTeamsMet,
                highDataModel.TermMedicalReviewAt, highDataModel.Specialty,
                new DateTime(highDataModel.PrescribedHighYear, highDataModel.PrescribedHighMonth, highDataModel.PrescribedHighDay),
                highDataModel.PersonWhoDeliveredTheSummary,
                new DateTime(highDataModel.DeliveredDateYear, highDataModel.DeliveredDateMonth, highDataModel.DeliveredDateDay)
                    );

            RefreshSessionSummary();
        }

        public JsonResult SpecialtyAutoComplete(string term)
        {
            var specialties = FactoryController.GetController(ControllerEnum.Specialty).GetSpecialty(term);

            var specialtyModels = specialties != null ? MapSpecialtyModelsFrom(specialties) : new List<SpecialtyModel>();

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

        public void DeleteComplementaryExam(int id)
        {
            if (id != 0)
            {
                var summary = GetSummary();
                var complementaryExam = summary.HighData.ComplementaryExams.Where(ce => ce.Id == id).ToList();

                summary.HighData.ComplementaryExams.Remove(complementaryExam.FirstOrDefault());

                FactoryController.GetController(ControllerEnum.Summary).SaveHighData
                (
                summary.Id,
                null,//todo: Idictionary
                summary.HighData.HighType,
                summary.HighData.ConditionOfThePatientAtHigh,
                summary.HighData.DestinationOfThePatientAtDischarge,
                summary.HighData.OrientationOfMultidisciplinaryTeamsMet,
                summary.HighData.TermMedicalReviewAt, summary.HighData.Specialty,
                new DateTime(summary.HighData.PrescribedHighYear, summary.HighData.PrescribedHighMonth, summary.HighData.PrescribedHighDay),
                summary.HighData.PersonWhoDeliveredTheSummary,
                new DateTime(summary.HighData.DeliveredDateYear, summary.HighData.DeliveredDateMonth, summary.HighData.DeliveredDateDay)
                    );

                RefreshSessionSummary();
            }
        }

        #endregion

        #region Private Methods

        private SummaryModel GetSummary()
        {
            return (SummaryModel)Session["Summary"];
        }

        private void SetSummary(SummaryModel summaryModel)
        {
            Session["Summary"] = summaryModel;
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
            Mapper.CreateMap<Summary, SummaryModel>().ForMember(hosp => hosp.Hospital, source => source.Ignore())
                .ForMember(al => al.Allergies, so => so.Ignore()).ForMember(di => di.Diagnostics, so => so.Ignore())
                .ForMember(proc => proc.Procedures, so => so.Ignore()).ForMember(hemo => hemo.Hemotransfusions, so => so.Ignore())
                .ForMember(ex => ex.Exams, so => so.Ignore()).ForMember(me => me.Medications, so => so.Ignore())
                .ForMember(hd => hd.HighData, so => so.Ignore());

            var summaryModel = Mapper.Map<Summary, SummaryModel>(summary);
            summaryModel.HighData = MapHighDataModelFrom(summary.HighData);
            summaryModel.Allergies = MapAllergyModelsFrom(summary.Allergies);
            summaryModel.Diagnostics = MapDiagnosticsModelsFrom(summary.Diagnostics);
            summaryModel.Procedures = MapProceduresModelsFrom(summary.Procedures);
            summaryModel.Medications = MapMedicationModelsFrom(summary.Medications);
            summaryModel.Hemotransfusions = MapHemotransfusionModelsFrom(summary.Hemotransfusions);
            summaryModel.Exams = MapExamModelsFrom(summary.Exams);

            return summaryModel;
        }

        public static HighDataModel MapHighDataModelFrom(HighData highData)
        {
            Mapper.CreateMap<HighData, HighDataModel>().ForMember(ec => ec.ComplementaryExams, source => source.Ignore())
                .ForMember(s => s.Specialty, source => source.Ignore());
            var highDataModel = Mapper.Map<HighData, HighDataModel>(highData);

            highDataModel.PrescribedHighYear = highData.PrescribedHigh.Value.Year;
            highDataModel.PrescribedHighMonth = highData.PrescribedHigh.Value.Month;
            highDataModel.PrescribedHighDay = highData.PrescribedHigh.Value.Day;

            highDataModel.DeliveredDateYear = highData.DeliveredDate.Value.Year;
            highDataModel.DeliveredDateMonth = highData.DeliveredDate.Value.Month;
            highDataModel.DeliveredDateDay = highData.DeliveredDate.Value.Day;


            return highDataModel;
        }

        private static List<HemotransfusionModel> MapHemotransfusionModelsFrom(IList<Hemotransfusion> hemotransfusions)
        {
            var hemoModels = new List<HemotransfusionModel>();
            foreach (var hemotransfusion in hemotransfusions)
            {
                Mapper.CreateMap<Hemotransfusion, HemotransfusionModel>();
                var hemotransfusionModel = Mapper.Map<Hemotransfusion, HemotransfusionModel>(hemotransfusion);
                hemotransfusionModel.HemotransfusionType = hemotransfusion.Type.Id;
                foreach (var reaction in hemotransfusion.Reactions)
                {
                    hemotransfusionModel.ReactionTypes.Add(reaction.Id);
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
                Mapper.CreateMap<Diagnostic, DiagnosticModel>().ForMember(type => type.Type, diag => diag.Ignore()).ForMember(cid => cid.Cid, diag => diag.Ignore());
                var diagnosticsModel = Mapper.Map<Diagnostic, DiagnosticModel>(diagnostic);
                diagnosticsModel.Type = diagnostic.Type.Id;
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

        private static List<DefModel> MapDefModelsFrom(IList<DefDTO> defs)
        {
            var defModels = new List<DefModel>();
            foreach (var def in defs)
            {
                var defModel = MapDefModelFrom(def);
                defModel.Code = def.Id;
                defModels.Add(defModel);
            }
            return defModels;
        }

        private static DefModel MapDefModelFrom(DefDTO def)
        {
            Mapper.CreateMap<DefDTO, DefModel>();
            return Mapper.Map<DefDTO, DefModel>(def);
        }

        private static DefModel MapDefModelFrom(Def def)
        {
            Mapper.CreateMap<Def, DefModel>();
            var defModel = Mapper.Map<Def, DefModel>(def);
            defModel.Code = def.Id;
            return defModel;
        }

        private static List<MedicationModel> MapMedicationModelsFrom(IList<Medication> medications)
        {
            var medicationsModel = new List<MedicationModel>();
            foreach (var medication in medications)
            {
                var medicationModel = MapMedicationModelFrom(medication);
                medicationsModel.Add(medicationModel);
            }
            return medicationsModel;
        }

        private static MedicationModel MapMedicationModelFrom(Medication medication)
        {
            Mapper.CreateMap<Medication, MedicationModel>().ForMember(def => def.Def, so => so.Ignore());
            var medicationModel = Mapper.Map<Medication, MedicationModel>(medication);
            medicationModel.Def = MapDefModelFrom(medication.Def);
            return medicationModel;
        }

        private static ExamModel MapExamModelFrom(Exam exam)
        {
            Mapper.CreateMap<Exam, ExamModel>();
            return Mapper.Map<Exam, ExamModel>(exam);
        }

        private static List<ExamModel> MapExamModelsFrom(IList<Exam> exams)
        {
            var examModels = new List<ExamModel>();
            foreach (var exam in exams)
            {
                examModels.Add(MapExamModelFrom(exam));
            }
            return examModels;
        }

        private static List<SpecialtyModel> MapSpecialtyModelsFrom(IList<Specialty> specialties)
        {
            var specialtyModels = new List<SpecialtyModel>();
            foreach (var specialty in specialties)
            {
                var specialtyModel = MapSpecialtyModelFrom(specialty);
                specialtyModel.Code = specialty.Id;
                specialtyModels.Add(specialtyModel);
            }
            return specialtyModels;
        }

        private static SpecialtyModel MapSpecialtyModelFrom(Specialty specialty)
        {
            Mapper.CreateMap<Specialty, SpecialtyModel>();
            return Mapper.Map<Specialty, SpecialtyModel>(specialty);
        }

        #endregion

        #endregion
    }
}