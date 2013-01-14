using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities.Patient
{

    public class Admission : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual IList<Reason> ReasonOfAdmission { get; set; }
    }

    public enum Reason
    {
        Eletiva = 1,
        Emergencia = 2,
        Clinica = 3,
        Cirurgica = 4
    }

}