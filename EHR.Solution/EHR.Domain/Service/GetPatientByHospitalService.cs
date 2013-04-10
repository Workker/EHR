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
            var servico = new GetPatientsLuceneService();
            return servico.GetPatients(patientDTO.Name);
        }

        public IList<IPatientDTO> AdvancedGetPatientBy(IPatientDTO patientDTO, List<string> hospital)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatientsAdvancedSearch(patientDTO,hospital);
        }
    }
}
