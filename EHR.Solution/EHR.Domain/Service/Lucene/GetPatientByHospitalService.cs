using EHR.CoreShared;
using EHR.CoreShared.Entities;
using EHR.CoreShared.Interfaces;
using EHR.Domain.Repository;
using EHRIntegracao.Domain.Services;
using EHRIntegracao.Domain.Services.GetEntities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EHR.Domain.Service.Lucene
{
    public class GetPatientByHospitalService
    {
        public IPatient GetPatientBy(string cpf)
        {
            var servico = new GetPatientsLuceneService();
            var patient = servico.GetPatientBy(cpf);
            TreatmentsLuceneService treatmentsService = new TreatmentsLuceneService();

            var treatments = treatmentsService.GetTreatments(patient.Records);
            if (treatments != null)
                patient.AddTreatments(treatments);

            var hospitals = new Types<Hospital>().All();

            patient.Treatments = new List<ITreatment>
                                     {
                                         new Treatment
                                             {
                                                 Id = "123",
                                                 Hospital = hospitals[3],
                                                 EntryDate = DateTime.Now.AddDays(-3),
                                                 CheckOutDate = DateTime.Now
                                             },
                                         new Treatment
                                             {
                                                 Id = "1234",
                                                 Hospital = hospitals[6],
                                                 EntryDate = DateTime.Now.AddMonths(-3).AddDays(-3),
                                                 CheckOutDate = DateTime.Now.AddMonths(-3)
                                             },
                                         new Treatment
                                             {
                                                 Id = "1235",
                                                 Hospital = hospitals[12],
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

        public IList<IPatient> AdvancedGetPatientBy(IPatient patient, IList<short> hospitals)
        {
            //todo: remover mock
            var listHospital = new List<string>();

            foreach (short hospital in hospitals)
            {
                listHospital.Add(hospital.ToString(CultureInfo.InvariantCulture));
            }

            var servico = new GetPatientsLuceneService();
            return servico.GetPatientsAdvancedSearch(patient, listHospital);
        }
        //todo: mock
        public IList<IPatient> MockPatients(string name)
        {
            var servico = new GetPatientsLuceneService();
            var patients = servico.MockPatients(name);

            //foreach (var patient in patients)
            //{
            //    patient.Treatments = new List<ITreatment>
            //                             {
            //                                 new Treatment
            //                                     {
            //                                         Hospital = DbEnum.CopaDor,
            //                                         EntryDate = DateTime.Now.AddDays(-3),
            //                                         CheckOutDate = DateTime.Now
            //                                     },
            //                                 new Treatment
            //                                     {
            //                                         Hospital = DbEnum.CopaDor,
            //                                         EntryDate = DateTime.Now.AddMonths(-3).AddDays(-3),
            //                                         CheckOutDate = DateTime.Now.AddMonths(-3)
            //                                     },
            //                                 new Treatment
            //                                     {
            //                                         Hospital = DbEnum.CopaDor,
            //                                         EntryDate = DateTime.Now.AddMonths(-3).AddDays(-5),
            //                                         CheckOutDate = DateTime.Now.AddMonths(-5)
            //                                     }
            //                             };
            //}

            return patients;
        }
    }
}
