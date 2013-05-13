using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class AllergyType : IAggregateRoot<short>
    {
        public virtual short Id { get; set; }
        public virtual string Description { get; set; }
    }
}
