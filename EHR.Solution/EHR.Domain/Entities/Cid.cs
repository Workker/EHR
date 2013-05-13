using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Cid : ValueObject
    {
        public virtual string Code { get; set; }
    }
}
