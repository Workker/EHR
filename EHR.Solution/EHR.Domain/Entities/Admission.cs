using EHR.CoreShared.Interfaces;
using System.Collections.Generic;

namespace EHR.Domain.Entities
{

    public class Admission : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual IList<ReasonOfAdmission> ReasonOfAdmission { get; set; }

    }
}