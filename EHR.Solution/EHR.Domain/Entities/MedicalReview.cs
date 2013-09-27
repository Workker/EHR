
namespace EHR.Domain.Entities
{
    public class MedicalReview
    {
        public virtual int Id { get; set; }
        public virtual int TermMedicalReviewAt { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}
