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
        public EHR.Controller.ProcedureController ProcedureController
        {
            get { return new EHR.Controller.ProcedureController(); }
        }

        #region Views

        public ActionResult Index(string cpf)
        {
            var controller = new EHR.Controller.PatientController();

            TreatPatient(cpf, controller);
            FillAlergies();
            FillDiagnostic();

            if (Session["Summary"] == null)
                Response.Redirect("/Home");


            return View();
        }

        private void TreatPatient(string cpf, Controller.PatientController controller)
        {
            var patient = controller.GetBy(cpf);
            ViewBag.data = patient;
            ViewBag.age = CalculateAgeFrom((DateTime)patient.DateBirthday);
            Session["Summary"] = controller.GetSummaryByPatient(patient);
            FillDiagnostic();
            FillAlergies();
        }

        private void FillHemotransfusion()
        {
            ViewBag.Hemotransfusions = Convert(GetSummary().Hemotransfusions);
        }

        private void FillDiagnostic()
        {
            ViewBag.Diagnostics = Convert(GetSummary().Diagnostics);
        }

        private void FillAlergies()
        {
            ViewBag.Allergies = Convert(GetSummary().Allergies);
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
            FillAlergies();
            FillDiagnostic();
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

        #endregion

        #region Diagnostic

        public PartialViewResult DiagnosticForm()
        {
            return PartialView("GeneralData/_DiagnosticsForm");
        }

        public PartialViewResult SaveDiagnostic(string type, string cidCode, string description)
        {
            FactoryController.GetController(ControllerEnum.Diagnostic).SaveDiagnostic(type, cidCode, GetSummary());
            ViewBag.Diagnostics = ConvertLast(GetSummary().Diagnostics);

            return PartialView("GeneralData/_DiagnosticTableRow");
        }

        public void DeleteDiagnostic(string id)
        {
            FactoryController.GetController(ControllerEnum.Diagnostic).RemoveDiagnostic(GetSummary(), int.Parse(id));
        }

        public JsonResult CidAutoComplete(string term)
        {
            var cids = FactoryController.GetController(ControllerEnum.Diagnostic).GetCids(term);

            return Json(cids, JsonRequestBehavior.AllowGet);
        }

        private dynamic ConvertLast(IList<Diagnostic> diagnostics)
        {
            var diagnosticsModels = new List<DiagnosticModel>();

            var diacnostic = new DiagnosticModel()
            {
                Id = diagnostics.Last().Id,
                Cid = ConvertCid(diagnostics.Last().Cid),
                Type = diagnostics.Last().Type.Id
            };

            diagnosticsModels.Add(diacnostic);

            return diagnosticsModels;
        }

        private dynamic Convert(IList<Diagnostic> diagnostics)
        {
            var diagnosticsModels = new List<DiagnosticModel>();

            foreach (var diagnostic in diagnostics)
            {
                diagnosticsModels.Add(new DiagnosticModel()
                {
                    Cid = ConvertCid(diagnostic.Cid)
                    ,
                    Id = diagnostic.Id
                    ,
                    Type = diagnostic.Type.Id
                });
            }
            return diagnosticsModels;
        }

        private CidModel ConvertCid(Cid cid)
        {
            return new CidModel() { Code = cid.Code, Description = cid.Description };
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

        public PartialViewResult SaveAllergy(string theWitch, List<string> type)
        {
            FactoryController.GetController(ControllerEnum.Allergy).SaveAllergy(theWitch, type.ConvertAll(short.Parse), GetSummary());
            ViewBag.Allergies = ConvertLast(GetSummary().Allergies);

            return PartialView("GeneralData/_AllergyTableRow");
        }

        public void DeleteAllergy(string id)
        {
            FactoryController.GetController(ControllerEnum.Allergy).RemoveAllergy(GetSummary(), int.Parse(id));
        }

        public List<AllergyModel> Convert(IList<Allergy> allergies)
        {
            var allergyModels = new List<AllergyModel>();

            foreach (var allergy in allergies)
            {
                var allergyModel = new AllergyModel()
                                       {
                                           Id = allergy.Id,
                                           TheWitch = allergy.TheWhich,
                                       };
                allergyModel.Types = new List<short>();
                foreach (var allergyType in allergy.Types)
                {
                    allergyModel.Types.Add(allergyType.Id);
                }
                allergyModels.Add(allergyModel);
            }
            return allergyModels;
        }

        public List<AllergyModel> ConvertLast(IList<Allergy> allergies)
        {
            var allergyModels = new List<AllergyModel>();

            var allergy = new AllergyModel()
                 {
                     Id = allergies.Last().Id,
                     TheWitch = allergies.Last().TheWhich,

                 };
            allergy.Types = new List<short>();
            foreach (var type in allergies.Last().Types)
            {
                allergy.Types.Add(type.Id);
            }

            allergyModels.Add(allergy);


            return allergyModels;
        }

        #endregion

        #region Procedure

        public PartialViewResult Procedures()
        {
            Summary summary = GetSummary();
            ViewBag.Procedures = Convert(summary.Procedures);
            return PartialView("_Procedures");
        }

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

        public JsonResult TusAutoComplete(string term)
        {
            List<TusDTO> tus = FactoryController.GetController(ControllerEnum.Procedure).GetTus(term);

            return Json(tus, JsonRequestBehavior.AllowGet);
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



        #endregion

        #region Hemotransfusion

        public PartialViewResult Hemotransfusion()
        {
            FillHemotransfusion();
            return PartialView("_hemotransfusion");
        }

        public PartialViewResult HemotransfusionForm()
        {
            return PartialView("Hemotransfusion/_HemotransfusionForm");
        }

        public PartialViewResult SaveHemotransfusion(List<string> typeReaction, string typeHemotrasfusion)
        {
            FactoryController.GetController(ControllerEnum.Hemotransfusion).SaveHemotransfusion(typeReaction, typeHemotrasfusion, GetSummary());
            ViewBag.Hemotransfusions = ConvertLast(GetSummary().Hemotransfusions);

            return PartialView("Hemotransfusion/_HemotransfusionTableRow");
        }

        public void DeleteHemotransfusion(string id)
        {
            FactoryController.GetController(ControllerEnum.Hemotransfusion).RemoveHemotransfusion(GetSummary(), int.Parse(id));
        }

        private dynamic ConvertLast(IList<Hemotransfusion> hemos)
        {
            var HemoModels = new List<HemotransfusionModel>();

            var hemoModel = new HemotransfusionModel()
            {
                Id = hemos.Last().Id,
                HemotransfusionType = hemos.Last().Type.Id,
            };

            hemoModel.ReactionType = new List<short>();

            foreach (var reaction in hemos.Last().Reactions)
            {
                hemoModel.ReactionType.Add(reaction.Id);
            }

            HemoModels.Add(hemoModel);

            return HemoModels;
        }

        private dynamic Convert(IList<Hemotransfusion> hemos)
        {
            var HemoModels = new List<HemotransfusionModel>();

            foreach (var hemo in hemos)
            {
                var hemoModel = new HemotransfusionModel()
                {
                    Id = hemo.Id,
                    HemotransfusionType = hemo.Type.Id,
                };

                hemoModel.ReactionType = new List<short>();

                foreach (var reaction in hemo.Reactions)
                {
                    hemoModel.ReactionType.Add(reaction.Id);
                }

                HemoModels.Add(hemoModel);
            }
            return HemoModels;
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

        #region support methods

        private int CalculateAgeFrom(DateTime birthday)
        {
            return DateTime.Today.Year - birthday.Year;
        }

        private Summary GetSummary()
        {
            return (Summary)Session["Summary"];
        }

        #endregion

    }
}