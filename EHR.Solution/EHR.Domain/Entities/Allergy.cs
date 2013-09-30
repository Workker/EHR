using System.Collections.Generic;

namespace EHR.Domain.Entities
{
    public class Allergy
    {
        public virtual int Id { get; set; }
        public virtual string TheWhich { get; set; }
        public virtual IList<AllergyType> Types { get; set; }
    }
}
