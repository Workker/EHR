using EHR.CoreShared.Interfaces;
using System.Collections.Generic;

namespace EHR.Domain.Entities
{
    public class Allergy : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string TheWhich { get; set; }
        public virtual IList<AllergyType> Types { get; set; }
    }
}
