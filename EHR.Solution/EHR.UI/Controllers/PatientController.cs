﻿using EHR.Controller;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Infrastructure.Util;
using EHR.UI.Filters;
using EHR.UI.Infrastructure.Notification;
using EHR.UI.Models;
using EHR.UI.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace EHR.UI.Controllers
{
    [AuthenticationFilter]
    public class PatientController : System.Web.Mvc.Controller
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

        public ActionResult Index(string cpf, string treatment)
        {
            try
            {
                var patient = FactoryController.GetController(ControllerEnum.Patient).GetBy(cpf);
                var patientModel = PatientMapper.MapPatientModelFrom(patient, treatment);
                var summary = FactoryController.GetController(ControllerEnum.Patient).GetSummaryBy(patient, treatment, GetAccount().Id);
                var allergies = FactoryController.GetController(ControllerEnum.Patient).GetAllergiesBy(patient.CPF);
                var medications = FactoryController.GetController(ControllerEnum.Patient).GetMedicationsOfUseAfterInternationBy(patient.CPF);

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

                ViewBag.AllAlergies = AllergyMapper.MapAllergyModelsFrom(allergies);
                ViewBag.AllMedications = MedicationMapper.MapMedicationModelsFrom(medications);
                ViewBag.Allergies = summaryModel.Allergies;
                ViewBag.Diagnostics = summaryModel.Diagnostics;
                ViewBag.Procedures = summaryModel.Procedures;
                ViewBag.Medications = summaryModel.Medications;

                return View(summaryModel);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
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

        public string Admission(string q)
        {
            try
            {
                var stringReturn = "[";

                foreach (var id in Enum.GetValues(typeof(ReasonOfAdmissionEnum)).Cast<short>().ToList())
                {
                    var description =
                        EnumUtil.GetDescriptionFromEnumValue(
                            (ReasonOfAdmissionEnum)Enum.Parse(typeof(ReasonOfAdmissionEnum), id.ToString(CultureInfo.InvariantCulture)));
                    if (description.Contains(q))
                    {
                        stringReturn += "{\"name\":\"" + description + "\",\"id\":\"" + id + "\"}, ";
                    }
                }
                stringReturn = stringReturn.Remove(stringReturn.Length - 2);
                stringReturn += "]";
                return stringReturn;
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
                return null;
            }
        }

        [HttpPost]
        public void SaveObservation(string observation)
        {
            try
            {
                FactoryController.GetController(ControllerEnum.Summary).SaveObservation(GetSummary().Id, observation);
                RefreshSessionSummary();

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

                this.ShowMessage(MessageTypeEnum.Success, "Medicamento incluído.");

                return PartialView("Medication/_TableRow");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        public void DeleteMedication(Medication medication)
        {
            try
            {
                SummaryController.RemoveMedication(GetSummary().Id, medication.Id);

                RefreshSessionSummary();

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

                RefreshSessionSummary();

                this.ShowMessage(MessageTypeEnum.Success, "Colonização por germes multiresistentes atualizado.");
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);
            }
        }

        #endregion

        #region High Data

        public PartialViewResult DataHigh()
        {
            try
            {
                var summary = GetSummary();

                Session["MedicalReviews"] = summary.HighData.MedicalReviews;
                Session["ComplementaryExams"] = summary.HighData.ComplementaryExams;

                ViewBag.MedicalReviews = GetMedicalReviewsFromSession();
                ViewBag.ComplementaryExams = GetComplementaryExamsFromSession();

                return PartialView("_DataHigh", summary);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageTypeEnum.Error, ex.Message);

                return null;
            }
        }

        [HttpPost]
        public void SaveHighData(HighDataModel highDataModel)
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

                FactoryController.GetController(ControllerEnum.Summary).SaveHighData
                    (
                    GetSummary().Id,
                    complementaryExam,
                    complementaryExamDeleteds,
                    medicalReview,
                    medicalReviewDeleteds,
                    highDataModel.HighType,
                    highDataModel.ConditionOfThePatientAtHigh,
                    highDataModel.DestinationOfThePatientAtDischarge,
                    highDataModel.OrientationOfMultidisciplinaryTeamsMet,
                    new DateTime(highDataModel.PrescribedHighYear, highDataModel.PrescribedHighMonth, highDataModel.PrescribedHighDay),
                    highDataModel.PersonWhoDeliveredTheSummary,
                    new DateTime(highDataModel.DeliveredDateYear, highDataModel.DeliveredDateMonth, highDataModel.DeliveredDateDay)
                        );

                RefreshSessionSummary();

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
            return PartialView("DataHigh/_MedicalReviewForm");
        }

        [HttpPost]
        public PartialViewResult SaveMedicalReview(MedicalReviewModel review)
        {
            try
            {
                AddOnSessionMedicalReviews(review);
                ViewBag.MedicalReviews = new List<MedicalReviewModel> { GetMedicalReviewsFromSession().Last() };

                this.ShowMessage(MessageTypeEnum.Success, "Prazo para revisão incluída.");

                return PartialView("DataHigh/_MedicalReviewTableRow");
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
            return PartialView("DataHigh/_ComplementaryExamForm");
        }

        [HttpPost]
        public PartialViewResult SaveComplementaryExam(ComplementaryExamModel complementaryExamModel)
        {
            try
            {
                AddOnSessionComplementaryExams(complementaryExamModel);

                ViewBag.ComplementaryExams = new List<ComplementaryExamModel> { GetComplementaryExamsFromSession().Last() };

                this.ShowMessage(MessageTypeEnum.Success, "Exame complementar incluído.");

                return PartialView("DataHigh/_ComplementaryExamTableRow");
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

        #endregion

        #region Private Methods

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

        private void RegisterView(Summary summary)
        {
            SummaryController.AddView(summary.Id, ((AccountModel)Session["account"]).Id, DateTime.Now);
        }

        private AccountModel GetAccount()
        {
            return (AccountModel)Session["account"];
        }

        private void RefreshSessionSummary()
        {
            var summary = SummaryController.GetBy(GetSummary().Id);

            Session["Summary"] = SummaryMapper.MapSummaryModelFrom(summary);
        }

        private IList<ViewModel> DistinctView(IEnumerable<ViewModel> viewModels)
        {
            return viewModels.GroupBy(x => x.Account.Id).Select(x => x.FirstOrDefault()).ToList();
        }

        #endregion
    }
}