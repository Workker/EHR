using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.CoreShared;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Record : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Observation { get; set; }

        public virtual IList<Admission> Admissions { get; set; }
        public virtual IList<Allergy> Allergies { get; set; }
        public virtual IList<Procedure> Procedures { get; set; }
        public virtual IList<Hemotransfusion> Hemotransfusions { get; set; }

        public virtual PatientDTO Patient { get; set; }
        public virtual TreatmentDTO Treatment { get; set; }
    }
}
