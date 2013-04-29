using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using EHR.Domain.Repository;
using EHR.Domain.Service;
using EHRIntegracao.Domain.Factorys;
using EHRIntegracao.Domain.Services.DTO;


namespace EHR.Controller
{
    public class PatientController
    {

        public PatientController()
        {
            // _patients = new Patients();
        }

        public IPatientDTO GetBy(string id)
        {
            return new PatientDTO
            {
                Name = "Joelma Santos Carvalho",
                Hospital = DbEnum.BarraDor,
                DateBirthday = new DateTime(1965,06,15),
                Id = 1.ToString()
            };
        }

        public IList<IPatientDTO> GetBy(DbEnum hospital, PatientDTO dto)
        {
            var service = new GetPatientByHospitalService();

            return service.GetPatientBy(hospital, dto);
        }

        public IList<IPatientDTO> GetBy(PatientDTO dto, List<string> hospital)
        {
            var service = new GetPatientByHospitalService();
            return service.AdvancedGetPatientBy(dto, hospital);
        }

    }
}
