using System;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;

namespace EHR.Controller
{
    public abstract class EHRController
    {
        private Summaries summaries;
        public Summaries Summaries
        {
            get { return summaries ?? (summaries = new Summaries()); }
            set
            {
                summaries = value;
            }
        }

        public virtual List<TusDTO> GetTus(string term) { return null; }
        public virtual List<CidDTO> GetCids(string term) { return null; }

        public virtual IList<Summary> GetLastTenSumaries(int id) { return null; }

        #region Patient

        public virtual IPatientDTO GetBy(string cpf) { return null; }
        public virtual IList<IPatientDTO> GetBy(PatientDTO dto) { return null; }
        public virtual IList<IPatientDTO> GetBy(PatientDTO dto, List<string> hospital) { return null; }

        #endregion

        #region Account

        public virtual void Register(string firstName, string lastName, GenderEnum gender, string crm, string email, string password, DateTime birthday, IList<short> hospitals) { }
        public virtual Account Login(string email, string password) { return null; }
        public virtual bool VerifyIfExist(string email) { return false; }
        public virtual IList<Account> GetAllNotApproved() { return null; }
        public virtual IList<Summary> GetSumaries(int id) { return null; }
        public virtual void ApproveAccount(int id) { }
        public virtual void RefuseAccount(int id) { }

        #endregion

        #region Procedure

        public virtual void SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, Summary summary) { }
        public virtual void RemoveProcedure(Summary summary, int id) { }

        #endregion

        #region Allergy

        public virtual void SaveAllergy(string theWitch, IList<short> types, Summary summary) { }
        public virtual void RemoveAllergy(Summary summary, int id) { }

        #endregion

        #region Diagnostic

        public virtual void SaveDiagnostic(string diagnosticType, string cid, Summary summary) { }
        public virtual void RemoveDiagnostic(Summary summary, int id) { }

        #endregion

        #region Hemotransfusion

        public virtual void SaveHemotransfusion(List<string> typeReaction, string typeHemotrasfusion, Summary summary) { }
        public virtual void RemoveHemotransfusion(Summary summary, int id) { }

        #endregion
    }
}
