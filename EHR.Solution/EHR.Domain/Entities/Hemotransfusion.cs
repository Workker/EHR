using EHR.Domain.Entities.Interfaces;
using System.Collections.Generic;

namespace EHR.Domain.Entities
{
    public class Hemotransfusion : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual ReactionType Type { get; set; }
        public virtual IList<ReactionType> Reactions { get; set; }
    }
}
