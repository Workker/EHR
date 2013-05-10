using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Tus : IAggregateRoot<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Procedure { get; set; }
    }
}
