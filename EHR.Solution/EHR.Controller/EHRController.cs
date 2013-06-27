using System;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;

namespace EHR.Controller
{
    public abstract class EHRController
    {
        private Summaries _summaries;
        public Summaries Summaries
        {
            get { return _summaries ?? (_summaries = new Summaries()); }
            set
            {
                _summaries = value;
            }
        }

        public virtual List<TusDTO> GetTus(string term) { return null; }
        public virtual List<CidDTO> GetCids(string term) { return null; }

        public virtual IList<Summary> GetLastTenSumaries(int id) { return null; }

        #region Patient

        public virtual IPatientDTO GetBy(string cpf) { return null; }
        public virtual IList<IPatientDTO> GetBy(PatientDTO dto) { return null; }
        public virtual IList<IPatientDTO> GetBy(PatientDTO dto, List<string> hospital) { return null; }
        public virtual Summary GetSummaryBy(IPatientDTO patient, string treatment, int idAccount) { return null; }

        #endregion

        #region Summary

        public virtual void SaveMdr(int summaryId, string mdr) { }
        public virtual Summary GetBy(int id) { return null; }

        #endregion

        #region Medication

        public virtual List<DefDTO> GetDef(string term) { return null; }
        public virtual void SaveMedication(int idSummary, short medicationType, short def, string presentation,
            string presentationType, string dose, string dosage, string way, string place, string frequency, string frequencyCase, int duration) { }
        public virtual void RemoveMedication(int idSummary, int id) { }

        #endregion

        #region Exam

        public virtual void SaveExam(int idSummary, short type, string day, string month, string year, string description) { }
        public virtual void RemoveExam(int idSummary, int id) { }

        #endregion

        #region Account

        public virtual void Register(string firstName, string lastName, short gender, string crm, string email, string password, DateTime birthday, IList<short> hospitals) { }
        public virtual Account Login(string email, string password) { return null; }
        public virtual bool VerifyIfExist(string email) { return false; }
        public virtual IList<Account> GetAllNotApproved() { return null; }
        public virtual IList<Summary> GetSumaries(int id) { return null; }
        public virtual void ApproveAccount(int id) { }
        public virtual void RefuseAccount(int id) { }
        public virtual void AlterPasswordOfAccount(int id, string password) { }

        #endregion

        #region Procedure

        public virtual void SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, int idSummary) { }
        public virtual void RemoveProcedure(int idSummary, int id) { }

        #endregion

        #region Allergy

        public virtual void SaveAllergy(string theWitch, IList<short> types, int idSummary) { }
        public virtual void RemoveAllergy(int idSummary, int id) { }

        #endregion

        #region Diagnostic

        public virtual void SaveDiagnostic(string diagnosticType, string cid, int idSummary) { }
        public virtual void RemoveDiagnostic(int idSummary, int id) { }

        #endregion

        #region Hemotransfusion

        public virtual void SaveHemotransfusion(List<string> typeReaction, string typeHemotrasfusion, int idSummary) { }
        public virtual void RemoveHemotransfusion(int idSummary, int id) { }

        #endregion

        #region HighData

        #region Specialty

        public virtual List<Specialty> GetSpecialty(string term) { return null; }
        public virtual Specialty GetById(short id) { return null; }

        #endregion

        public virtual void SaveHighData(int idSummary, IDictionary<short, IDictionary<string, int>> complementaryExams, short highType,
           short conditionOfThePatientAtHigh, short destinationOfThePatientAtDischarge,
          short orientationOfMultidisciplinaryTeamsMet, int termMedicalReviewAt, short specialty, DateTime prescribedHigh,
           string personWhoDeliveredTheSummary, DateTime deliveredDate) { }

        #endregion
    }
}
