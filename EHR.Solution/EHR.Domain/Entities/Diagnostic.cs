using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities.Patient
{
    public class Diagnostic : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string CidCode { get; set; }
        public virtual string Cid { get; set; }
    }
}
