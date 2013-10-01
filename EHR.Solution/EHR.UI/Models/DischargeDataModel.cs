using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class DischargeDataModel
    {
        public int Id { get; set; }
        public short HighType { get; set; }
        public short ConditionAtDischarge { get; set; }
        public short DestinationOfThePatientAtDischarge { get; set; }
        public short MultidisciplinaryTeamsMet { get; set; }
        public string PersonWhoDeliveredTheSummary { get; set; }
        private List<MedicalReviewModel> _medicalReviews;
        public List<MedicalReviewModel> MedicalReviews
        {
            get { return _medicalReviews ?? (_medicalReviews = new List<MedicalReviewModel>()); }
        }
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