using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public virtual DateTime Date { get; set; }

        public virtual Admission Admission { get; set; }
        public virtual IList<Allergy> Allergies { get; set; }
        public virtual IList<Procedure> Procedures { get; set; }
        public virtual IList<Hemotransfusion> Hemotransfusions { get; set; }
        
        public virtual IPatientDTO Patient { get; set; }
        public virtual ITreatmentDTO Treatment { get; set; }

        public virtual void RemoveProcedure(int id)
        {
            var procedure = Procedures.FirstOrDefault(p => p.Id == id);

            if (procedure != null)
                Procedures.Remove(procedure);
        }
    }
}
