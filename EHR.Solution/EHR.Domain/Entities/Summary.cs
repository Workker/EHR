using EHR.CoreShared;
using EHR.Domain.Entities.Interfaces;
using System.Collections.Generic;

namespace EHR.Domain.Entities
{
    public class Summary : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Observation { get; set; }
        public virtual string Cpf { get; set; }

        public virtual IList<Admission> Admissions { get; set; }
        public virtual IList<Allergy> Allergies { get; set; }
        public virtual IList<Procedure> Procedures { get; set; }
        public virtual IList<Hemotransfusion> Hemotransfusions { get; set; }
        
        public virtual PatientDTO Patient { get; set; }
        public virtual TreatmentDTO Treatment { get; set; }
    }
}
