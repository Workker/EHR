using EHR.CoreShared;
using EHRIntegracao.Domain.Services;
using System.Collections.Generic;
using EHRIntegracao.Domain.Services.GetEntities;

namespace EHR.Domain.Service
{
    public class GetPatientByHospitalService
    {
        public IPatientDTO GetPatientBy(string cpf)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatientBy(cpf);
        }

        public IList<IPatientDTO> GetPatientBy(IPatientDTO patientDTO)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatients(patientDTO.Name);
        }

        public IList<IPatientDTO> AdvancedGetPatientBy(IPatientDTO patientDTO, List<string> hospital)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatientsAdvancedSearch(patientDTO, hospital);
        }
    }
}
