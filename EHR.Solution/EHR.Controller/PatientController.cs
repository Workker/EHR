using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service;
using System.Collections.Generic;


namespace EHR.Controller
{
    public class PatientController : EHRController
    {

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

        public Summary GetSummaryByPatient(IPatientDTO patient)
        {
            Summaries summaries = new Summaries();

            var summary = summaries.GetLastSummary(patient.CPF);
            if (summary != null)
                summary.Patient = patient;
            return summary;
        }



        public void RemoveProcedure(Summary summary, int id)
        {
            throw new System.NotImplementedException();
        }


        public List<Tus> GetTus()
        {
            throw new System.NotImplementedException();
        }


        public void SaveProcedure(string dob_day, string dob_month, string dob_year, string procedureCode, Summary summary)
        {
            throw new System.NotImplementedException();
        }
    }
}
