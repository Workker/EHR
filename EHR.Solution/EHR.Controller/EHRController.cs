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

        public virtual IPatientDTO GetBy(string cpf) { return null; }
        public virtual IList<IPatientDTO> GetBy(PatientDTO dto) { return null; }
        public virtual IList<IPatientDTO> GetBy(PatientDTO dto, List<string> hospital) { return null; }

        public virtual List<TusDTO> GetTus(string term) { return null; }

        public virtual void SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, Summary summary) { }
        public virtual void RemoveProcedure(Summary summary, int id) { }

        public virtual void SaveAllergy(string theWitch, IList<short> types, Summary summary) { }
        public virtual void RemoveAllergy(Summary summary, int id) { }

        public virtual List<CidDTO> GetCids(string term) { return null; }

        public virtual void SaveDiagnostic(string diagnosticType, string cid, Summary summary) { }
        public virtual void RemoveDiagnostic(Summary summary, int id) { }

        public virtual void SaveHemotransfusion(List<string> typeReaction, string typeHemotrasfusion, Summary summary) { }
        public virtual void RemoveHemotransfusion(Summary summary, int id) { }

        public virtual void Register(string firstName, string lastName, string gender, string crm, string email, string password, DateTime birthday) { }
    }
}
