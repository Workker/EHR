using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHRIntegracao.Domain.Factorys;
using EHRIntegracao.Domain.Services;
using EHRIntegracao.Domain.Services.DTO;

namespace EHR.Domain.Service
{
    public class GetPatientByHospitalService
    {
        public IList<IPatientDTO> GetPatientBy(DbEnum hospital,IPatientDTO patientDTO)
        {
            //var service = new PatientIntegrationService();
            //return service.GetAllPatients(patientDTO, hospital);

            List<IPatientDTO> pacientes = new List<IPatientDTO>();
            pacientes.Add(new PatientDTO() { Name = "Sammuel" });
            pacientes.Add(new PatientDTO() { Name = "Sammuel Garcia" });
            pacientes.Add(new PatientDTO() { Name = "Thiago" });
            pacientes.Add(new PatientDTO() { Name = "Thiago Oliveira" });
            
            return pacientes.Where(n => n.Name.Contains(patientDTO.Name)).ToList();
        }
    }
}
