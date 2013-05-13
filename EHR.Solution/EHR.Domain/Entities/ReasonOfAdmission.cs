using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class ReasonOfAdmission : IAggregateRoot<short>
    {
        public virtual short Id { get; set; }
        public virtual string Description { get; set; }
    }
}
