namespace EHR.Domain.Entities
{
    public class ComplementaryExam
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual int Period { get; set; }
    }
}
