using EHR.CoreShared;
using EHR.CoreShared.Interfaces;
using EHRIntegracao.Domain.Services.GetEntities;
using System;
using System.Collections.Generic;

namespace EHR.Domain.Service.Lucene
{
    public class GetPatientByHospitalService
    {
        public IPatient GetPatientBy(string cpf)
        {
            var servico = new GetPatientsLuceneService();
            var patient = servico.GetPatientBy(cpf);
            //Todo: Remover esta merda depois
            patient.Treatments = new List<ITreatment>
                                     {
                                         new Treatment
                                             {
                                                 Id = "123",
                                                 Hospital = DbEnum.CopaDor,
                                                 EntryDate = DateTime.Now.AddDays(-3),
                                                 CheckOutDate = DateTime.Now
                                             },
                                         new Treatment
                                             {
                                                 Id = "1234",
                                                 Hospital = DbEnum.Esperanca,
                                                 EntryDate = DateTime.Now.AddMonths(-3).AddDays(-3),
                                                 CheckOutDate = DateTime.Now.AddMonths(-3)
                                             },
                                         new Treatment
                                             {
                                                 Id = "1235",
                                                 Hospital = DbEnum.QuintaDor,
                                                 EntryDate = DateTime.Now.AddMonths(-3).AddDays(-5),
                                                 CheckOutDate = DateTime.Now.AddMonths(-5)
                                             }
                                     };
            return patient;
        }

        public IList<IPatient> GetPatientBy(IPatient patient)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatients(patient.Name);
        }

        public IList<IPatient> GetPatientAll()
        {
            var service = new GetPatientsService();
            return service.GetPatientsDbFor();
        }

        public IList<IPatient> AdvancedGetPatientBy(IPatient patient, List<string> hospital)
        {
            var servico = new GetPatientsLuceneService();
            return servico.GetPatientsAdvancedSearch(patient, hospital);
        }
        //todo: mock
        public IList<IPatient> MockPatients(string name)
        {
            var servico = new GetPatientsLuceneService();
            var patients = servico.MockPatients(name);

            foreach (var patient in patients)
            {
                patient.Treatments = new List<ITreatment>
                                         {
                                             new Treatment
                                                 {
                                                     Hospital = DbEnum.CopaDor,
                                                     EntryDate = DateTime.Now.AddDays(-3),
                                                     CheckOutDate = DateTime.Now
                                                 },
                                             new Treatment
                                                 {
                                                     Hospital = DbEnum.CopaDor,
                                                     EntryDate = DateTime.Now.AddMonths(-3).AddDays(-3),
                                                     CheckOutDate = DateTime.Now.AddMonths(-3)
                                                 },
                                             new Treatment
                                                 {
                                                     Hospital = DbEnum.CopaDor,
                                                     EntryDate = DateTime.Now.AddMonths(-3).AddDays(-5),
                                                     CheckOutDate = DateTime.Now.AddMonths(-5)
                                                 }
                                         };
            }

            return patients;
        }
    }
}
