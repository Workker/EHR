using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using EHR.Domain.Repository;

namespace EHR.Controller
{
    public class PatientController
    {
        private readonly Patients _patients;

        public PatientController()
        {
            _patients = new Patients();
        }

        public Patient GetBy(int id)
        {
            return _patients.GetBy(id);
        }

        public void Add(Patient patient)
        {
            _patients.Save(patient);
        }

    }
}
