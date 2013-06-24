using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportLegacySummary.DTO
{
    public class Summary
    {
        public Summary()
        {
            Diagnostics = new List<Diagnostic>();
            Procedures = new List<Procedure>();
            Medications = new List<Medication>();
            Hemotransfusions = new List<Hemotransfusion>();
        }

        public long Id { get; set; }
        public string Observation { get; set; }
        public DateTime? EntryDateTreatment { get; set; }
        public string CodeMedicalRecord { get; set; }
        public string Hospital { get; set; }
        public string Cpf { get; set; }
        public string Admission { get; set; }
        public string AllergyMed { get; set; }
        public string AllergyType { get; set; }

        public IList<Diagnostic> Diagnostics { get; set; }
        public IList<Procedure> Procedures { get; set; }
        public IList<Medication> Medications { get; set; }
        public IList<Hemotransfusion> Hemotransfusions { get; set; }
    }
}
