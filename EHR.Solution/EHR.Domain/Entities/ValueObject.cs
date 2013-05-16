using EHR.Domain.Entities.Interfaces;
using System;

namespace EHR.Domain.Entities
{
    [Serializable]
    public abstract class ValueObject : IAggregateRoot<short>
    {
        public virtual short Id { get; set; }
        public virtual string Description { get; set; }
    }
}
