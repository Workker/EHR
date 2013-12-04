using EHR.CoreShared.Interfaces;
using EHRIntegracao.Domain.Services;
using EHRIntegracao.Domain.Services.GetEntities;
using System.Collections.Generic;
using System.Globalization;

namespace EHR.Domain.Service.Lucene
{
    public class GetPatientByHospitalService
    {
        public IPatient GetPatientBy(string cpf)
        {
            var servico = new GetPatientsLuceneService();
            var recordsService = new GetRecordsLuceneService();
            var treatmentsService = new TreatmentsLuceneService();

            var patient = servico.GetPatientBy(cpf);

            patient.Records = recordsService.GetRecordBy(cpf);

            var treatments = treatmentsService.GetTreatments(patient.Records);

            if (treatments != null)
                patient.AddTreatments(treatments);

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
    }
}
