using EHR.CoreShared;
using EHRIntegracao.Domain.Services.GetEntities;
using System;
using System.Collections.Generic;

namespace EHR.Domain.Service
{
    public class GetPatientByHospitalService
    {
        public IPatientDTO GetPatientBy(string cpf)
        {
            var servico = new GetPatientsLuceneService();
            var patient = servico.GetPatientBy(cpf);
            //Todo: Remover esta merda depois
            patient.Treatments = new List<ITreatmentDTO>();
            patient.Treatments.Add(new TreatmentDTO() { Id = "123", Hospital = DbEnum.Copa, EntryDate = DateTime.Now.AddDays(-3), CheckOutDate = DateTime.Now });
            patient.Treatments.Add(new TreatmentDTO() { Id = "1234", Hospital = DbEnum.Copa, EntryDate = DateTime.Now.AddMonths(-3).AddDays(-3), CheckOutDate = DateTime.Now.AddMonths(-3) });
            patient.Treatments.Add(new TreatmentDTO() { Id = "1235", Hospital = DbEnum.Copa, EntryDate = DateTime.Now.AddMonths(-3).AddDays(-5), CheckOutDate = DateTime.Now.AddMonths(-5) });
            return patient;
        }

        public IList<IPatientDTO> GetPatientBy(IPatientDTO patientDTO)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatients(patientDTO.Name);
        }

        public IList<IPatientDTO> GetPatientAll()
        {
            GetPatientsService service = new GetPatientsService();
            return service.GetPatientsDbFor();
        }


        public IList<IPatientDTO> AdvancedGetPatientBy(IPatientDTO patientDTO, List<string> hospital)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatientsAdvancedSearch(patientDTO, hospital);
        }
        //todo: mock
        public IList<IPatientDTO> MockPatients(string name)
        {
            var servico = new GetPatientsLuceneService();
            var patients = servico.MockPatients(name);

            foreach (var patient in patients)
            {
                patient.Treatments = new List<ITreatmentDTO>();
                patient.Treatments.Add(new TreatmentDTO() { Hospital = DbEnum.Copa, EntryDate = DateTime.Now.AddDays(-3), CheckOutDate = DateTime.Now });
                patient.Treatments.Add(new TreatmentDTO() { Hospital = DbEnum.Copa, EntryDate = DateTime.Now.AddMonths(-3).AddDays(-3), CheckOutDate = DateTime.Now.AddMonths(-3) });
                patient.Treatments.Add(new TreatmentDTO() { Hospital = DbEnum.Copa, EntryDate = DateTime.Now.AddMonths(-3).AddDays(-5), CheckOutDate = DateTime.Now.AddMonths(-5) });
            }

            return patients;
        }

    }
}
