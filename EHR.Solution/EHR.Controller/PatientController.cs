using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using EHR.Domain.Repository;
using EHR.Domain.Service;
using EHRIntegracao.Domain.Factorys;
using EHRIntegracao.Domain.Services.DTO;
using PatientDTO = EHR.Domain.PatientDTO;

namespace EHR.Controller
{
    public class PatientController
    {
        private readonly Patients _patients;

        public PatientController()
        {
           // _patients = new Patients();
        }

        public Patient GetBy(int id)
        {
            return _patients.GetBy(id);
        }

        public void Add(Patient patient)
        {
            _patients.Save(patient);
        }

        public IList<IPatientDTO> GetBy(DbEnum hospital, PatientDTO dto)
        {
            var service = new GetPatientByHospitalService();

            return service.GetPatientBy(hospital, dto);
        }

    }
}
