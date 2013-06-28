using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class HighDataModel
    {
        public int Id { get; set; }
        public short HighType { get; set; }
        public short ConditionOfThePatientAtHigh { get; set; }
        public short DestinationOfThePatientAtDischarge { get; set; }
        public short OrientationOfMultidisciplinaryTeamsMet { get; set; }
        public int TermMedicalReviewAt { get; set; }
        public SpecialtyModel Specialty { get; set; }
        public string PersonWhoDeliveredTheSummary { get; set; }
        private List<ComplementaryExamModel> _complementaryExams;
        public List<ComplementaryExamModel> ComplementaryExams
        {
            get { return _complementaryExams ?? (_complementaryExams = new List<ComplementaryExamModel>()); }
        }
        public int PrescribedHighMonth { get; set; }
        public int PrescribedHighYear { get; set; }
        public int PrescribedHighDay { get; set; }
        public int DeliveredDateMonth { get; set; }
        public int DeliveredDateYear { get; set; }
        public int DeliveredDateDay { get; set; }
    }
}