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
        public short Specialty { get; set; }
        public string PersonWhoDeliveredTheSummary { get; set; }
        private IDictionary<short, IDictionary<string, int>> _complementaryExams;
        public IDictionary<short, IDictionary<string, int>> ComplementaryExams
        {
            get { return _complementaryExams ?? (_complementaryExams = new Dictionary<short, IDictionary<string, int>>()); }
        }
        public int PrescribedHighMonth { get; set; }
        public int PrescribedHighYear { get; set; }
        public int PrescribedHighDay { get; set; }

        public int DeliveredDateMonth { get; set; }
        public int DeliveredDateYear { get; set; }
        public int DeliveredDateDay { get; set; }
    }
}