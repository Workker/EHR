using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Drug : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
    }
}
