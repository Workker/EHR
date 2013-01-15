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
        #region Properties

        public virtual int Id { get; set; }
        public virtual IList<Admission> Admissions { get; set; }
        public virtual IList<Allergy> Allergies { get; set; }
        public virtual IList<Diagnostic> Diagnostics { get; set; }
        public virtual String MedicinesOfUsePrior { get; set; }
        public virtual String Annotations { get; set; }

        #endregion

        #region Methods

        public virtual void AddAdmission(Admission admission)
        {
            if (Admissions == null)
                Admissions = new List<Admission>();
            Admissions.Add(admission);
        }

        public virtual  void AddAllergy(Allergy allergy)
        {
            if (Allergies == null)
                Allergies = new List<Allergy>();
            Allergies.Add(allergy);
        }

        public virtual void AddDiagnostic(Diagnostic diagnostic)
        {
            if (Diagnostics == null)
                Diagnostics = new List<Diagnostic>();
            Diagnostics.Add(diagnostic);
        }

        #endregion
    }
}
