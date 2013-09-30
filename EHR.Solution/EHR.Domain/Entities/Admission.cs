using System.Collections.Generic;

namespace EHR.Domain.Entities
{

    public class Admission
    {
        public virtual int Id { get; set; }
        public virtual IList<ReasonOfAdmission> ReasonOfAdmission { get; set; }

    }
}