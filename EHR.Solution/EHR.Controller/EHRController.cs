using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;

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

        public IPatientDTO GetBy(string cpf) { return null; }
        public IList<IPatientDTO> GetBy(DbEnum hospital, PatientDTO dto) { return null; }
        public IList<IPatientDTO> GetBy(PatientDTO dto, List<string> hospital) { return null; }

        public List<Tus> GetTus() { return null; }

        public void SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, Summary summary) { }
        public void RemoveProcedure(Summary summary, int id) { }

        public void SaveAllergy(string theWitch, IList<AllergyType> types, Summary summary) { }
        public void RemoveAllergy(Summary summary, int id) { }
    }
}
