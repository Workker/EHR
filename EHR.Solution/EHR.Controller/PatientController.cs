using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service;
using System.Collections.Generic;


namespace EHR.Controller
{
    public class PatientController : EHRController
    {
        public override IPatientDTO GetBy(string cpf)
        {
            var service = new GetPatientByHospitalService();
            return service.GetPatientBy(cpf);
        }

        public override IList<IPatientDTO> GetBy(PatientDTO dto)
        {
            var service = new GetPatientByHospitalService();

            return service.GetPatientBy(dto);
        }

        public override IList<IPatientDTO> GetBy(PatientDTO dto, List<string> hospital)
        {
            var service = new GetPatientByHospitalService();
            return service.AdvancedGetPatientBy(dto, hospital);
        }

        public Summary GetSummaryByPatient(IPatientDTO patient)
        {
            var summaries = new Summaries();

            var summary = summaries.GetLastSummary(patient.CPF);
            if (summary != null)
                summary.Patient = patient;
            return summary;
        }
    }
}
