using System.Linq;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using EHR.Domain.Service;
using System;
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

        public override Summary GetSummaryBy(IPatientDTO patient, string treatment, int idAccount)
        {
            var summaries = new Summaries();
            var accounts = new Accounts();

            Summary summary;

            summary = GetSummary(patient, treatment, summaries);

            if (summary != null)
                summary.Patient = patient;
            else
            {
                if (patient.Treatments != null && patient.Treatments.Count > 0)
                {
                    summary = CreateMedicalRecord(patient, idAccount, accounts, treatment);
                    summaries.Save(summary);
                }
            }

            return summary;
        }

        private static Summary CreateMedicalRecord(IPatientDTO patient, int Id, Accounts accounts, string treatment)
        {
            Summary summary;
            var account = accounts.GetBy(Id);
            var treatmentDTO = patient.Treatments.OrderByDescending(t => t.EntryDate).FirstOrDefault();
            summary = new Summary()
                          {
                              Cpf = patient.CPF,
                              Date = DateTime.Now,
                              Treatment = patient.Treatments.OrderByDescending(t => t.EntryDate).FirstOrDefault(),
                              CodeMedicalRecord = string.IsNullOrEmpty(treatment) ? treatmentDTO.Id : treatment,
                              Account = account
                          };
            return summary;
        }

        private static Summary GetSummary(IPatientDTO patient, string treatment, Summaries summaries)
        {
            Summary summary;
            if (string.IsNullOrEmpty(treatment))
                summary = summaries.GetLastSummary(patient.CPF);
            else
                summary = summaries.GetSummaryByTreatment(patient.CPF, treatment);
            return summary;
        }

    }
}
