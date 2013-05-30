
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Hospital : IAggregateRoot<short>
    {
        public virtual short Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string URLImage { get; set; }
    }
}
