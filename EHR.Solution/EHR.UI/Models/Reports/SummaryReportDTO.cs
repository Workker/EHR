
namespace EHR.UI.Models.Reports
{
    public class SummaryReportDTO
    {
        public string PatientName { get; set; }
        public string PatientGender { get; set; }
        public string PatientAge { get; set; }
        public string PatientBirthday { get; set; }
        public string RecordCode { get; set; }
        public string HospitalName { get; set; }
        public string AccoutName { get; set; }
        public string ProfissionalRegistrationNumber { get; set; }
        public string State { get; set; }
        public string EntryDate { get; set; }
        public string DischargeDate { get; set; }
        public string InpatientDays { get; set; }
        public string ResonOfAdmission { get; set; }
        public string SummaryDate { get; set; }
        public string MDR { get; set; }
        public string Observation { get; set; }
        public string ConditionAtDischarge { get; set; }
        public string DestinationOfThePatientAtDischarge { get; set; }
        public string MultidisciplinaryTeamsMet { get; set; }
        public string PersonWhoDeliveredTheSummary { get; set; }
        public string DeliveredDate { get; set; }
        public int PrescribedDischargeDate { get; set; }
    }
}