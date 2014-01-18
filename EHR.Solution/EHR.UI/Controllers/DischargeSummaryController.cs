using System.Globalization;
using EHR.Controller;
using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
using EHR.Infrastructure.Service.Report;
using EHR.Infrastructure.Util;
using EHR.UI.Filters;
using EHR.UI.Infrastructure.Notification;
using EHR.UI.Models;
using EHR.UI.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EHR.UI.Models.Reports;
using Microsoft.Reporting.WebForms;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class DischargeSummaryController : System.Web.Mvc.Controller
    {
        private static EhrController AllergyController
        {
            get { return FactoryController.GetController(ControllerEnum.Allergy); }
        }
        private static EhrController DiagnosticController
        {
            get { return FactoryController.GetController(ControllerEnum.Diagnostic); }
        }
        private static EhrController SummaryController
        {
            get { return FactoryController.GetController(ControllerEnum.Summary); }
        }

        public ActionResult Index(string cpf, string treatment, string hospitalId)
        {
            try
            {
                var patient = FactoryController.GetController(ControllerEnum.Patient).GetBy(cpf);
                var patientModel = PatientMapper.MapPatientModelFrom(patient, treatment);
                var summary = FactoryController.GetController(ControllerEnum.Patient).GetSummaryBy(patient, treatment, GetAccount().Id);
                var allergies = FactoryController.GetController(ControllerEnum.Patient).GetAllergiesBy(patient.CPF);
                var medications = FactoryController.GetController(ControllerEnum.Patient).GetMedicationsOfUseAfterInternationBy(patient.CPF);

                var summaryModel = SummaryMapper.MapSummaryModelFrom(summary);

                summaryModel.Patient = patientModel;
                Session["Summary"] = summaryModel;
                Session["Patient"] = patientModel;

                if (GetSummary() == null)
                    Response.Redirect("/Home");


                var resonsOfAdmission = FactoryController.GetController(ControllerEnum.Types).GetReasonsOfAdmission();
                ViewBag.ResonsOfAdmission = ReasonsOfAdmissionMapper.MapReasonsOfAdmissionModelFrom(resonsOfAdmission);

                ViewBag.LastVisitors = summaryModel.Views;
                ViewBag.LastActions = summaryModel.Actions;

                ViewBag.AllAlergies = AllergyMapper.MapAllergyModelsFrom(allergies);
                ViewBag.AllMedications = MedicationMapper.MapMedicationModelsFrom(medications);
                ViewBag.Allergies = summaryModel.Allergies;
                ViewBag.Diagnostics = summaryModel.Diagnostics;
                ViewBag.Procedures = summaryModel.Procedures;
                ViewBag.Medications = summaryModel.Medications;

                this.RegisterActionOfUser(HistoricalActionTypeEnum.View);

                return View(summaryModel);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

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
            try
            {
                var medications = GetSummary().Medications;
                ViewBag.Medications = medications;
                return PartialView("_Prescriptions");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        #endregion

        #region General Data

        public PartialViewResult GeneralData()
        {
            try
            {
                var summary = GetSummary();

                var resonsOfAdmission = FactoryController.GetController(ControllerEnum.Types).GetReasonsOfAdmission();
                ViewBag.ResonsOfAdmission = ReasonsOfAdmissionMapper.MapReasonsOfAdmissionModelFrom(resonsOfAdmission);
                ViewBag.Allergies = summary.Allergies;
                ViewBag.Diagnostics = summary.Diagnostics;
                ViewBag.Medications = summary.Medications;

                return PartialView("_GeneralData", summary);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        public void SaveReasonOfAdmission(List<short> reasonOfAdmission)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).SaveReasonOfAdmission(GetSummary().Id, reasonOfAdmission);
                RefreshSessionSummary();
                this.ShowMessage(MessageTypeEnum.Success, "Motivo de admissao atualizado.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        [HttpPost]
        public void SaveObservation(string observation)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).SaveObservation(GetSummary().Id, observation);

                RefreshSessionSummary();

                this.RegisterActionOfUser(
                    string.IsNullOrEmpty(GetSummary().Observation)
                        ? HistoricalActionTypeEnum.Include
                        : HistoricalActionTypeEnum.Change, "observacao");

                this.ShowMessage(MessageTypeEnum.Success, "História, exame fisico na admissão, breve curso hospitalar e exames relevantes. Atualizado.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        #region Allergy

        public PartialViewResult AllergyForm()
        {
            return PartialView("GeneralData/_AllergyForm");
        }

        public PartialViewResult SaveAllergy(string theWitch, List<string> type)
        {
            try
            {
                AllergyController.SaveAllergy(theWitch, type.ConvertAll(short.Parse), GetSummary().Id);

                RefreshSessionSummary();

                ViewBag.Allergies = new List<AllergyModel> { GetSummary().Allergies.Last() };

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "alergia");

                this.ShowMessage(MessageTypeEnum.Success, "Alergia incluída.");

                return PartialView("GeneralData/_AllergyTableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        public void DeleteAllergy(string id)
        {
            try
            {
                AllergyController.RemoveAllergy(GetSummary().Id, int.Parse(id));

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Exclude, "alergia");

                this.ShowMessage(MessageTypeEnum.Success, "Alergia excluida.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        #endregion

        #region Diagnostic

        public PartialViewResult DiagnosticForm()
        {
            return PartialView("GeneralData/_DiagnosticsForm");
        }

        public PartialViewResult SaveDiagnostic(DiagnosticModel diagnostic)
        {
            try
            {
                DiagnosticController.SaveDiagnostic(diagnostic.Type, diagnostic.Description, diagnostic.Cid.Code, GetSummary().Id);

                RefreshSessionSummary();

                ViewBag.Diagnostics = new List<DiagnosticModel> { GetSummary().Diagnostics.Last() };

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "diagnóstico");

                this.ShowMessage(MessageTypeEnum.Success, "Diagnóstico incluído.");

                return PartialView("GeneralData/_DiagnosticTableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public void DeleteDiagnostic(string id)
        {
            try
            {
                DiagnosticController.RemoveDiagnostic(GetSummary().Id, int.Parse(id));

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Exclude, "diagnóstico");

                this.ShowMessage(MessageTypeEnum.Success, "Diagnóstico excluído.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        public JsonResult CidAutoComplete(string term)
        {
            try
            {
                var cids = DiagnosticController.GetCids(term);

                return Json(cids, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        #endregion

        #endregion

        #region Medication

        public PartialViewResult MedicationForm(string medicationType)
        {
            try
            {
                ViewBag.MedicationType = medicationType;

                return PartialView(short.Parse(medicationType) != 3 ? "Medication/_SimpleForm" : "Medication/_CompleteForm");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        [HttpPost]
        public PartialViewResult SaveMedication(MedicationModel medication)
        {
            try
            {
                SummaryController.SaveMedication(GetSummary().Id, medication.Type, medication.Def.Id, medication.Description, medication.Presentation,
                    medication.PresentationType, medication.Dose, medication.Dosage,
                    medication.Way, medication.Place, medication.Frequency, medication.FrequencyCase, medication.Duration);

                RefreshSessionSummary();

                ViewBag.Medications = new List<MedicationModel> { GetSummary().Medications.Last() };
                ViewBag.MedicationType = medication.Type;

                switch (medication.Type)
                {
                    case 1:
                        this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "medicamento utilizado antes da internacao");
                        break;
                    case 2:
                        this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "medicamento utilizado durante a internacao");
                        break;
                    case 3:
                        this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "medicamento na prescricao de alta");
                        break;
                }

                this.ShowMessage(MessageTypeEnum.Success, "Medicamento incluído.");

                return PartialView("Medication/_TableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public void DeleteMedication(MedicationModel medication)
        {
            try
            {
                SummaryController.RemoveMedication(GetSummary().Id, medication.Id);

                RefreshSessionSummary();

                switch (medication.Type)
                {
                    case 1:
                        this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "medicamento utilizado antes da internacao");
                        break;
                    case 2:
                        this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "medicamento utilizado durante a internacao");
                        break;
                    case 3:
                        this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "medicamento na prescricao de alta");
                        break;
                }

                this.ShowMessage(MessageTypeEnum.Success, "Medicamento excluído.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        public JsonResult DefAutoComplete(string term)
        {
            try
            {
                var defDtOs = FactoryController.GetController(ControllerEnum.Def).GetDef(term);

                var defModels = DefMapper.MapDefModelsFrom(defDtOs);

                return Json(defModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        #endregion

        #region Procedure

        public PartialViewResult Procedures()
        {
            try
            {
                SummaryModel summary = GetSummary();
                ViewBag.Procedures = summary.Procedures;
                return PartialView("_Procedures");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public PartialViewResult ProcedureForm()
        {
            return PartialView("Procedure/_ProcedureForm");
        }

        public PartialViewResult SaveProcedure(string day, string month, string year, string procedureCode, string procedure, string description)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Procedure).SaveProcedure(day, month, year, procedureCode, GetSummary().Id, description);

                RefreshSessionSummary();
                ViewBag.Procedures = new List<ProcedureModel> { GetSummary().Procedures.Last() };

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "procedimento");

                this.ShowMessage(MessageTypeEnum.Success, "Procedimento incluído.");

                return PartialView("Procedure/_ProcedureTableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public void DeleteProcedure(int id)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Procedure).RemoveProcedure(GetSummary().Id, id);

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Exclude, "procedimento");

                this.ShowMessage(MessageTypeEnum.Success, "Procedimento excluído.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        public JsonResult TusAutoComplete(string term)
        {
            try
            {
                List<TUSS> tus = FactoryController.GetController(ControllerEnum.Procedure).GetTus(term);

                return Json(tus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        #endregion

        #region Exams

        public PartialViewResult Exams()
        {
            try
            {
                ViewBag.Exams = GetSummary().Exams;
                return PartialView("_Exams");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public PartialViewResult ExamForm()
        {
            return PartialView("Exams/_ExamsForm");
        }

        public PartialViewResult SaveExam(string type, int day, int month, int year, string description)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).SaveExam(GetSummary().Id, short.Parse(type), new DateTime(year, month, day), description);

                RefreshSessionSummary();
                ViewBag.Exams = new List<ExamModel> { GetSummary().Exams.Last() };

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "exame");

                this.ShowMessage(MessageTypeEnum.Success, "Exame incluído.");

                return PartialView("Exams/_ExamsTableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public void DeleteExam(int id)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).RemoveExam(GetSummary().Id, id);

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Exclude, "exame");

                this.ShowMessage(MessageTypeEnum.Success, "Exame excluído.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        #endregion

        #region Hemotransfusion

        public PartialViewResult Hemotransfusion()
        {
            try
            {
                ViewBag.Hemotransfusions = GetSummary().Hemotransfusions;

                return PartialView("_hemotransfusion");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public PartialViewResult HemotransfusionForm()
        {
            return PartialView("Hemotransfusion/_HemotransfusionForm");
        }

        public PartialViewResult SaveHemotransfusion(List<short> typeReaction, short typeHemotrasfusion)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Hemotransfusion).SaveHemotransfusion(typeReaction ?? new List<short>(),
                                                                                                    typeHemotrasfusion,
                                                                                                    GetSummary().Id);
                RefreshSessionSummary();
                ViewBag.Hemotransfusions = new List<HemotransfusionModel> { GetSummary().Hemotransfusions.Last() };

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "hemotransfusao");

                this.ShowMessage(MessageTypeEnum.Success, "Hemotransfusão incluída.");

                return PartialView("Hemotransfusion/_HemotransfusionTableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public void DeleteHemotransfusion(string id)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Hemotransfusion).RemoveHemotransfusion(GetSummary().Id,
                                                                                                      int.Parse(id));

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Exclude, "hemotransfusao");

                this.ShowMessage(MessageTypeEnum.Success, "Hemotransfusão excluída.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        #endregion

        #region MDR

        public PartialViewResult ColonizationbyMdr()
        {
            try
            {
                ViewBag.Mdr = GetSummary().Mdr;
                return PartialView("_ColonizationbyMDR");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        [HttpPost]
        public void SaveMdr(string mdr)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).SaveMdr(GetSummary().Id, mdr);

                this.RegisterActionOfUser(
                    string.IsNullOrEmpty(GetSummary().Mdr)
                        ? HistoricalActionTypeEnum.Include
                        : HistoricalActionTypeEnum.Change, "colonização por germes multiresistentes");


                RefreshSessionSummary();

                this.ShowMessage(MessageTypeEnum.Success, "Colonização por germes multiresistentes atualizado.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        #endregion

        #region Discharge Data

        public PartialViewResult DischargeData()
        {
            try
            {
                var summary = GetSummary();

                Session["MedicalReviews"] = summary.DischargeData.MedicalReviews;
                Session["ComplementaryExams"] = summary.DischargeData.ComplementaryExams;

                ViewBag.MedicalReviews = GetMedicalReviewsFromSession();
                ViewBag.ComplementaryExams = GetComplementaryExamsFromSession();

                return PartialView("_DischargeData", summary);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        [HttpPost]
        public void SaveHighData(DischargeDataModel dischargeDataModel)
        {
            try
            {
                var complementaryExam = new List<ComplementaryExam>();
                var complementaryExamDeleteds = new List<int>();

                foreach (var complementaryExamModel in GetComplementaryExamsFromSession())
                {
                    if (complementaryExamModel.Id == 0)
                    {
                        complementaryExam.Add(new ComplementaryExam
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

                var medicalReview = new List<MedicalReview>();
                var medicalReviewDeleteds = new List<int>();

                foreach (var medicalReviewModel in GetMedicalReviewsFromSession())
                {
                    if (medicalReviewModel.Id == 0)
                    {
                        medicalReview.Add(new MedicalReview
                        {
                            Specialty = new Specialty { Id = medicalReviewModel.Specialty.Id, Description = medicalReviewModel.Specialty.Description },
                            TermMedicalReviewAt = medicalReviewModel.TermMedicalReviewAt
                        });
                    }

                    if (medicalReviewModel.Deleted)
                    {
                        medicalReviewDeleteds.Add(medicalReviewModel.Id);
                    }
                }

                FactoryController.GetController(ControllerEnum.Summary).SaveDischargeData
                    (
                    GetSummary().Id,
                    complementaryExam,
                    complementaryExamDeleteds,
                    medicalReview,
                    medicalReviewDeleteds,
                    dischargeDataModel.HighType,
                    dischargeDataModel.ConditionAtDischarge,
                    dischargeDataModel.DestinationOfThePatientAtDischarge,
                    dischargeDataModel.MultidisciplinaryTeamsMet,
                    new DateTime(dischargeDataModel.PrescribedHighYear, dischargeDataModel.PrescribedHighMonth, dischargeDataModel.PrescribedHighDay, int.Parse(dischargeDataModel.PrescribedDischargeTime.Substring(0, 2)), int.Parse(dischargeDataModel.PrescribedDischargeTime.Substring(3, 2)), 0),
                    dischargeDataModel.PersonWhoDeliveredTheSummary,
                    new DateTime(dischargeDataModel.DeliveredDateYear, dischargeDataModel.DeliveredDateMonth, dischargeDataModel.DeliveredDateDay)
                        );

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Include, "dados de alta");

                this.ShowMessage(MessageTypeEnum.Success, "Dados de alta salvo.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        public JsonResult SpecialtyAutoComplete(string term)
        {
            try
            {
                var specialties = FactoryController.GetController(ControllerEnum.Specialty).GetSpecialty(term);

                var specialtyModels = specialties != null ? SpecialtyMapper.MapSpecialtyModelsFrom(specialties) : new List<SpecialtyModel>();

                return Json(specialtyModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public PartialViewResult MedicalReviewForm()
        {
            return PartialView("DischargeData/_MedicalReviewForm");
        }

        [HttpPost]
        public PartialViewResult SaveMedicalReview(MedicalReviewModel review)
        {
            try
            {
                if (review.Specialty.Id <= int.MinValue)
                {
                    AddOnSessionMedicalReviews(review);
                    ViewBag.MedicalReviews = new List<MedicalReviewModel> { GetMedicalReviewsFromSession().Last() };

                    this.ShowMessage(MessageTypeEnum.Success, "Prazo para revisão incluída.");

                    return PartialView("DischargeData/_MedicalReviewTableRow");
                }
                this.ShowMessage(MessageTypeEnum.Error, "Especialidade não informada.");
                return null;
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        [HttpPost]
        public void DeleteMedicalReview(int id)
        {
            try
            {
                if (id != 0)
                {
                    var medicalReviews = GetMedicalReviewsFromSession();

                    medicalReviews.Find(c => c.Id == id).Deleted = true;

                    Session["MedicalReviews"] = medicalReviews;
                }

                RefreshSessionSummary();

                this.ShowMessage(MessageTypeEnum.Success, "Prazo para revisão excluido.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        public PartialViewResult ComplementaryExamForm()
        {
            return PartialView("DischargeData/_ComplementaryExamForm");
        }

        [HttpPost]
        public PartialViewResult SaveComplementaryExam(ComplementaryExamModel complementaryExamModel)
        {
            try
            {
                AddOnSessionComplementaryExams(complementaryExamModel);

                ViewBag.ComplementaryExams = new List<ComplementaryExamModel> { GetComplementaryExamsFromSession().Last() };

                this.ShowMessage(MessageTypeEnum.Success, "Exame complementar incluído.");

                return PartialView("DischargeData/_ComplementaryExamTableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        [HttpPost]
        public void DeleteComplementaryExam(int id)
        {
            try
            {
                if (id != 0)
                {
                    var complementaryExam = GetComplementaryExamsFromSession();

                    complementaryExam.Find(c => c.Id == id).Deleted = true;

                    Session["ComplementaryExams"] = complementaryExam;
                }

                RefreshSessionSummary();

                this.ShowMessage(MessageTypeEnum.Success, "Exame complementar excluído.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        [HttpPost]
        public void FinalizeSummary()
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).FinalizeSummary(GetSummary().Id);

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Closed, "sumario");

                this.ShowMessage(MessageTypeEnum.Success, "Sumario Finalizado.");
            }
            catch (Exception exception)
            {

                this.ShowMessage(MessageTypeEnum.Error, exception.Message);
            }
        }

        [HttpPost]
        public void ReOpenSummary()
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).ReOpenSummary(GetSummary().Id);

                RefreshSessionSummary();

                this.RegisterActionOfUser(HistoricalActionTypeEnum.Reopened, "sumario");

                this.ShowMessage(MessageTypeEnum.Success, "Sumario Reaberto.");
            }
            catch (Exception exception)
            {

                this.ShowMessage(MessageTypeEnum.Error, exception.Message);
            }
        }

        #endregion

        #region Reports

        public ActionResult GeneratePrescriptionsReport()
        {
            var summary = GetSummary();

            var prescriptionDtOs = SetDataToPrescriptionReportDTO(summary);
            var summaryReportDtOs = SetDataToSummaryReportDTO(summary);

            var report = new ReportGenerationService("Report/Prescriptions.rdlc");

            var prescriptionDataSource = report.CreateReportDataSource(prescriptionDtOs, "Prescription");
            var summaryDataSource = report.CreateReportDataSource(summaryReportDtOs, "Summary");
            var dataSources = new List<ReportDataSource> { prescriptionDataSource, summaryDataSource };

            return File(GenerateReportFile(report, dataSources, ReportGenerationService.ReportType.pdf), "application/pdf");
        }

        public ActionResult GenerateSummayReport()
        {
            var summary = GetSummary();
            var report = new ReportGenerationService("Report/Summary.rdlc");

            var summaryReportDtOs = SetDataToSummaryReportDTO(summary);
            var allergyReportDtOs = SetDataToAllergyReportDTO(summary);
            var diagnosticReportDtOs = SetDataToDiagnosticReportDTO(summary);
            var medicationUseBeforeReportDtOs = SetDataToMedicationUseBeforeReportDTO(summary);
            var medicationUseDuringReportDtOs = SetDataToMedicationUseDuringReportDTO(summary);
            var examReportDtOs = SetDataToExamReportDTO(summary);
            var procedureReportDtOs = SetDataToProcedureReportDTO(summary);
            var hemotransfusionReportDtOs = SetDataTohemotransfusionReportDTO(summary);
            var prescriptionDtOs = SetDataToPrescriptionReportDTO(summary);
            var medicalReviewDtOs = SetDataToMedicalReviewReportDTO(summary);
            var complementaryExamDtOs = SetDataTocomplementaryExamReportDTO(summary);

            var summaryDataSource = report.CreateReportDataSource(summaryReportDtOs, "Summary");
            var allergyDataSource = report.CreateReportDataSource(allergyReportDtOs, "Allergy");
            var diagnosticDataSource = report.CreateReportDataSource(diagnosticReportDtOs, "Diagnostic");
            var medicationUseBeforeDataSource = report.CreateReportDataSource(medicationUseBeforeReportDtOs, "MedicationUseBefore");
            var medicationUseDuringDataSource = report.CreateReportDataSource(medicationUseDuringReportDtOs, "MedicationUseDuring");
            var examDataSource = report.CreateReportDataSource(examReportDtOs, "Exam");
            var procedureDataSource = report.CreateReportDataSource(procedureReportDtOs, "Procedure");
            var hemotransfusionDataSource = report.CreateReportDataSource(hemotransfusionReportDtOs, "Hemotransfusion");
            var prescriptionDataSource = report.CreateReportDataSource(prescriptionDtOs, "Prescription");
            var medicalReviewDataSource = report.CreateReportDataSource(medicalReviewDtOs, "MedicalReview");
            var complementaryExamDataSource = report.CreateReportDataSource(complementaryExamDtOs, "ComplementaryExam");

            var dataSources = new List<ReportDataSource> { summaryDataSource, allergyDataSource, diagnosticDataSource, medicationUseBeforeDataSource,
                medicationUseDuringDataSource, examDataSource, procedureDataSource, hemotransfusionDataSource, prescriptionDataSource, medicalReviewDataSource,complementaryExamDataSource };

            return File(GenerateReportFile(report, dataSources, ReportGenerationService.ReportType.pdf), "application/pdf");
        }

        #endregion

        #region Private Methods

        private List<ComplementaryExamReportDTO> SetDataTocomplementaryExamReportDTO(SummaryModel summary)
        {
            var complementaryExamReportDtOs = new List<ComplementaryExamReportDTO>();

            foreach (var complementaryExam in summary.DischargeData.ComplementaryExams)
            {
                var complementaryExamReportDtO = new ComplementaryExamReportDTO
                {
                    Description = complementaryExam.Description,
                    Period = complementaryExam.Period + " dia(s)"
                };

                complementaryExamReportDtOs.Add(complementaryExamReportDtO);
            }

            return complementaryExamReportDtOs;
        }

        private List<MedicalReviewReportDTO> SetDataToMedicalReviewReportDTO(SummaryModel summary)
        {
            var medicalReviewReportDtOs = new List<MedicalReviewReportDTO>();

            foreach (var medicalReview in summary.DischargeData.MedicalReviews)
            {
                var mediacalReviewDtO = new MedicalReviewReportDTO
                {
                    Specialty = medicalReview.Specialty.Description,
                    TermMedicalReviewAt = medicalReview.TermMedicalReviewAt + " dia(s)"
                };

                medicalReviewReportDtOs.Add(mediacalReviewDtO);
            }

            return medicalReviewReportDtOs;
        }

        private List<HemotransfusionReportDTO> SetDataTohemotransfusionReportDTO(SummaryModel summary)
        {
            var hemotransfusionReportDtOs = new List<HemotransfusionReportDTO>();

            foreach (var hemotransfusion in summary.Hemotransfusions)
            {
                var reactions = "";

                if (hemotransfusion.ReactionTypes != null)
                {
                    foreach (var reaction in hemotransfusion.ReactionTypes)
                    {
                        reactions +=
                            EnumUtil.GetDescriptionFromEnumValue(
                                (ReactionTypeEnum)
                                Enum.Parse(typeof(ReactionTypeEnum), reaction.ToString(CultureInfo.InvariantCulture))) + ", ";
                    }
                    reactions = reactions.Remove(reactions.Length - 2, 2);
                }

                var hemotransfusionReportDtO = new HemotransfusionReportDTO
                {
                    Type = EnumUtil.GetDescriptionFromEnumValue((HemotransfusionTypeEnum)Enum.Parse(typeof(HemotransfusionTypeEnum), hemotransfusion.HemotransfusionType.ToString(CultureInfo.InvariantCulture))),
                    ReAction = reactions
                };

                hemotransfusionReportDtOs.Add(hemotransfusionReportDtO);
            }

            return hemotransfusionReportDtOs;
        }

        private List<ProcedureReportDTO> SetDataToProcedureReportDTO(SummaryModel summary)
        {
            var procedureReportDtOs = new List<ProcedureReportDTO>();

            foreach (var procedure in summary.Procedures)
            {
                var procedureReportDtO = new ProcedureReportDTO
                {
                    Date = procedure.Date.ToShortDateString(),
                    Description = procedure.TUSS == null ? procedure.Description : procedure.TUSS.Description
                };

                procedureReportDtOs.Add(procedureReportDtO);
            }
            return procedureReportDtOs;
        }

        private List<ExamReportDTO> SetDataToExamReportDTO(SummaryModel summary)
        {
            var examReportDtOs = new List<ExamReportDTO>();

            foreach (var exam in summary.Exams)
            {
                var examReportDtO = new ExamReportDTO
                {
                    Date = exam.Date.ToShortDateString(),
                    Description = exam.Description,
                    Type = EnumUtil.GetDescriptionFromEnumValue((ExamTypeEnum)Enum.Parse(typeof(ExamTypeEnum), exam.Type.ToString(CultureInfo.InvariantCulture)))
                };

                examReportDtOs.Add(examReportDtO);
            }


            return examReportDtOs;
        }

        private List<PrescriptionReportDTO> SetDataToMedicationUseDuringReportDTO(SummaryModel summary)
        {
            var medicationUsingDuringReportDtOs = new List<PrescriptionReportDTO>();

            foreach (var medication in summary.Medications.Where(m => m.Type == 2))
            {
                var medicationUsingDuringReportDtO = new PrescriptionReportDTO
                {
                    DEFDescription = medication.Def.Description,
                    Duration = medication.Duration + " Dia(s)"
                };

                medicationUsingDuringReportDtOs.Add(medicationUsingDuringReportDtO);
            }

            return medicationUsingDuringReportDtOs;
        }

        private List<PrescriptionReportDTO> SetDataToMedicationUseBeforeReportDTO(SummaryModel summary)
        {
            var medicationUseBeforeReportDtOs = new List<PrescriptionReportDTO>();

            foreach (var medication in summary.Medications.Where(m => m.Type == 1))
            {
                var medicationUseBeforeDtO = new PrescriptionReportDTO
                {
                    DEFDescription = medication.Def.Description,
                    Duration = medication.Duration + " dia(s)"
                };
                medicationUseBeforeReportDtOs.Add(medicationUseBeforeDtO);
            }

            return medicationUseBeforeReportDtOs;
        }

        private List<DiagnosticReportDTO> SetDataToDiagnosticReportDTO(SummaryModel summary)
        {
            var diagnosticReportDtOs = new List<DiagnosticReportDTO>();

            foreach (var diagnostic in summary.Diagnostics)
            {
                var diagnosticDto = new DiagnosticReportDTO
                {
                    Type = EnumUtil.GetDescriptionFromEnumValue((DiagnosticTypeEnum)Enum.Parse(typeof(DiagnosticTypeEnum), diagnostic.Type.ToString(CultureInfo.InvariantCulture))),
                    CIDCode = diagnostic.Cid != null ? diagnostic.Cid.Code : "",
                    CIDDescription = diagnostic.Cid != null ? diagnostic.Cid.Description : diagnostic.Description
                };
                diagnosticReportDtOs.Add(diagnosticDto);
            }

            return diagnosticReportDtOs;
        }

        private List<AllergyReportDTO> SetDataToAllergyReportDTO(SummaryModel summary)
        {
            var allergyReportDTOs = new List<AllergyReportDTO>();

            foreach (var allegy in summary.Allergies)
            {
                var types = "";

                foreach (var type in allegy.Types)
                {
                    types +=
                        EnumUtil.GetDescriptionFromEnumValue(
                            (AllergyTypeEnum)
                            Enum.Parse(typeof(AllergyTypeEnum), type.ToString(CultureInfo.InvariantCulture))) + ", ";
                }

                types = types.Remove(types.Length - 2, 2);

                var allergyReportDtO = new AllergyReportDTO
                {
                    TheWhich = allegy.TheWhich,
                    Types = types
                };

                allergyReportDTOs.Add(allergyReportDtO);
            }

            return allergyReportDTOs;
        }

        private Byte[] GenerateReportFile(ReportGenerationService report, List<ReportDataSource> dataSources, ReportGenerationService.ReportType reportType)
        {
            return report.GenerateReport(dataSources, reportType);
        }

        private List<SummaryReportDTO> SetDataToSummaryReportDTO(SummaryModel summary)
        {

            var resonsOfAdmission = "";

            foreach (var reson in summary.ReasonsOfAdmission)
            {
                resonsOfAdmission += reson.Description + ", ";
            }

            resonsOfAdmission = resonsOfAdmission.Remove(resonsOfAdmission.Length - 2, 2);

            var summaryReportDtOs = new List<SummaryReportDTO>
                {
                    new SummaryReportDTO
                        {
                            PatientName = summary.Patient.Name,
                            //PatientGender = summary.Patient.Genre.ToString(),
                            PatientAge = summary.Patient.GetAge().ToString(CultureInfo.InvariantCulture),
                            PatientBirthday = summary.Patient.DateBirthday.ToShortDateString(),
                            RecordCode = summary.CodeMedicalRecord,
                            HospitalName = summary.Hospital.Description + " " + summary.Hospital.Name,
                            EntryDate = summary.Treatment.EntryDate.ToString(CultureInfo.InvariantCulture),
                            DischargeDate = summary.Treatment.CheckOutDate.ToString(CultureInfo.InvariantCulture),
                            InpatientDays = (summary.Treatment.CheckOutDate - summary.Treatment.EntryDate).TotalDays.ToString(CultureInfo.InvariantCulture),
                            ResonOfAdmission = resonsOfAdmission,
                            ConditionAtDischarge = EnumUtil.GetDescriptionFromEnumValue((ConditionOfThePatientAtHighEnum)Enum.Parse(typeof(ConditionOfThePatientAtHighEnum), summary.DischargeData.ConditionAtDischarge.ToString(CultureInfo.InvariantCulture))),
                            DestinationOfThePatientAtDischarge = EnumUtil.GetDescriptionFromEnumValue((DestinationOfThePatientAtDischargeEnum)Enum.Parse(typeof(DestinationOfThePatientAtDischargeEnum), summary.DischargeData.DestinationOfThePatientAtDischarge.ToString(CultureInfo.InvariantCulture))),
                            Observation = summary.Observation,
                            MDR = summary.Mdr,
                            MultidisciplinaryTeamsMet = EnumUtil.GetDescriptionFromEnumValue((OrientationOfMultidisciplinaryTeamsMetEnum)Enum.Parse(typeof(OrientationOfMultidisciplinaryTeamsMetEnum), summary.DischargeData.MultidisciplinaryTeamsMet.ToString(CultureInfo.InvariantCulture))),
                            AccoutName = summary.Account.FirstName + " " + summary.Account.LastName,
                            //ProfissionalRegistrationNumber = (summary.Account.ProfessionalRegistration.Where(p => p.State.Id == summary.Hospital.State.Id)).FirstOrDefault().Number,
                            SummaryDate = summary.Date.ToString(),
                            PersonWhoDeliveredTheSummary = summary.DischargeData.PersonWhoDeliveredTheSummary,
                            DeliveredDate = summary.DischargeData.DeliveredDateDay +"/"+ summary.DischargeData.DeliveredDateMonth + "/"+summary.DischargeData.DeliveredDateYear,
                            State = summary.Hospital.State.Description
                        }
                };
            return summaryReportDtOs;
        }

        private List<PrescriptionReportDTO> SetDataToPrescriptionReportDTO(SummaryModel summary)
        {
            var prescriptionDtOs = new List<PrescriptionReportDTO>();

            foreach (var medication in summary.Medications.Where(m => m.Type == 3))
            {
                var prescriptionDTO = new PrescriptionReportDTO
                {
                    DEFDescription = medication.Def.Description,
                    Presentation =
                        medication.Presentation + " " +
                        @EnumUtil.GetDescriptionFromEnumValue(
                            (PresentationTypeEnum)
                            Enum.Parse(typeof(PresentationTypeEnum),
                                       medication.PresentationType.ToString(CultureInfo.InvariantCulture))),
                    Dose =
                        medication.Dose + " " +
                        EnumUtil.GetDescriptionFromEnumValue(
                            (DosageEnum)
                            Enum.Parse(typeof(DosageEnum), medication.Dosage.ToString(CultureInfo.InvariantCulture))),
                    Way =
                        EnumUtil.GetDescriptionFromEnumValue(
                            (WayEnum)Enum.Parse(typeof(WayEnum), medication.Way.ToString(CultureInfo.InvariantCulture))),
                    Frequency =
                        EnumUtil.GetDescriptionFromEnumValue(
                            (FrequencyEnum)
                            Enum.Parse(typeof(FrequencyEnum), medication.Frequency.ToString(CultureInfo.InvariantCulture))),
                    Duration = medication.Duration + " Dia(s)"
                };
                if (string.IsNullOrEmpty(medication.Place))
                {
                    prescriptionDTO.Way += " " + medication.Place;
                }

                if (medication.FrequencyCase > 0)
                {
                    if (((short[])Enum.GetValues(typeof(FrequencyCaseEnum))).ToList().Contains(medication.FrequencyCase))
                    {
                        prescriptionDTO.Frequency += " " +
                                                     EnumUtil.GetDescriptionFromEnumValue(
                                                         (FrequencyCaseEnum)
                                                         Enum.Parse(typeof(FrequencyCaseEnum),
                                                                    medication.FrequencyCase.ToString(CultureInfo.InvariantCulture)));
                    }
                }

                prescriptionDtOs.Add(prescriptionDTO);
            }
            return prescriptionDtOs;
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

        private void AddOnSessionMedicalReviews(MedicalReviewModel medicalReviewModel)
        {
            var medicalReviewModels = GetMedicalReviewsFromSession();
            medicalReviewModels.Add(medicalReviewModel);
            Session["MedicalReviews"] = medicalReviewModels;
        }

        private List<MedicalReviewModel> GetMedicalReviewsFromSession()
        {
            return (List<MedicalReviewModel>)Session["MedicalReviews"];
        }

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
            var summary = SummaryController.GetBy(GetSummary().Id);
            var summaryModel = SummaryMapper.MapSummaryModelFrom(summary);

            summaryModel.Patient = (PatientModel)Session["Patient"];
            Session["Summary"] = summaryModel;
        }

        #endregion
    }
}