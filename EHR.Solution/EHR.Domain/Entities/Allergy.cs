using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Allergy : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string TheWhich { get; set; }
        public virtual IList<AllergyTypeEnum> Type { get; set; }
    }
}
