using System.Collections.Generic;
using EHR.Domain.Entities;

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
        public short Specialty { get; set; }
        public string PersonWhoDeliveredTheSummary { get; set; }
        private IList<ComplementaryExamModel> _complementaryExams;
        public IList<ComplementaryExamModel> ComplementaryExams
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