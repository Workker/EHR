namespace EHR.UI.Models
{
    public class MedicalReviewModel
    {
        public int Id { get; set; }
        public int TermMedicalReviewAt { get; set; }
        public SpecialtyModel Specialty { get; set; }
        public bool Deleted { get; set; }
    }
}