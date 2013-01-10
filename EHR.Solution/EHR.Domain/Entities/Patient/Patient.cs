using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities.Patient
{
    public class Patient : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual Admission Admission { get; set; }
        public virtual List<Allergy> Allergies { get; set; }
        public virtual List<Diagnostic> Diagnostics { get; set; }
        public virtual String MedicinesOfUsePrior { get; set; }
        public virtual String Annotations { get; set; }
    }
}
