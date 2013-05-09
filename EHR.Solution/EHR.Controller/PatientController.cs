using EHR.CoreShared;
using EHR.Domain.Service;
using System.Collections.Generic;


namespace EHR.Controller
{
    public class PatientController
    {

        public PatientController()
        {
            // _patients = new Patients();
        }

        public IPatientDTO GetBy(string cpf)
        {
            var service = new GetPatientByHospitalService();
            return service.GetPatientBy(cpf);
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
