using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class ComplementaryExam : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual int Period { get; set; }
    }
}
