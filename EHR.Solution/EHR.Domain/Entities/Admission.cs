using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{

    public class Admission : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual IList<Reason> ReasonOfAdmission { get; set; }
        
    }

    public enum Reason
    {
        Elective = 1,
        Emergency = 2,
        Clinic = 3,
        Cirurgic = 4
    }

}