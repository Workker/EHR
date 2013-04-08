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

            var servico = new GetPatientsLuceneService();
            return servico.GetPatients(patientDTO.Name);

            //var service = new PatientIntegrationService();
            //return service.GetAllPatients(patientDTO, hospital);

            //var pacientes = new List<IPatientDTO>
            //                    {
            //                        new PatientDTO() {Name = "Sammuel"},
            //                        new PatientDTO() {Name = "Sammuel Garcia"},
            //                        new PatientDTO() {Name = "Thiago"},
            //                        new PatientDTO() {Name = "Thiago Oliveira"}
            //                    };

            //return pacientes.Where(n => n.Name.Contains(patientDTO.Name)).ToList();
        }
    }
}
