using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities.Sumario
{
    public class Cid : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string CodigoCid { get; set; }


    }
}
