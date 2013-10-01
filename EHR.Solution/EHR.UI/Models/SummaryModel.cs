using System;
using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class SummaryModel
    {
        public int Id { get; set; }
        public HospitalModel Hospital { get; set; }
        public AccountModel Account { get; set; }
        public PatientModel Patient { get; set; }
        public TreatmentModel Treatment { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? EntryDateTreatment { get; set; }
        public string CodeMedicalRecord { get; set; }
        public string Mdr { get; set; }
        public string Observation { get; set; }
        public DischargeDataModel DischargeData { get; set; }
        public List<AllergyModel> Allergies { get; set; }
        public List<DiagnosticModel> Diagnostics { get; set; }
        public List<ProcedureModel> Procedures { get; set; }
        public List<MedicationModel> Medications { get; set; }
        public List<HemotransfusionModel> Hemotransfusions { get; set; }
        public IList<ExamModel> Exams { get; set; }
    }
}