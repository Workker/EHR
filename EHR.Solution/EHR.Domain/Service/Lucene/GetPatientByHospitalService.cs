using System.Configuration;
using EHR.CoreShared.Entities;
using EHR.CoreShared.Interfaces;
using EHR.Domain.Repository;
using EHRIntegracao.Domain.Services;
using EHRIntegracao.Domain.Services.GetEntities;
using System.Collections.Generic;

namespace EHR.Domain.Service.Lucene
{
    public class GetPatientByHospitalService
    {
        private readonly List<Hospital> _hospitals;
        private readonly GetPatientsLuceneService _patientsLuceneServiceservice;
        private readonly GetRecordsLuceneService _recordsLuceneServiceservice;
        private readonly TreatmentsLuceneService _treatmentsLuceneServiceservice;

        public GetPatientByHospitalService()
        {
            _hospitals = ((Hospitals)FactoryRepository.GetRepository(RepositoryEnum.Hospitals)).GetAllCached();
            _patientsLuceneServiceservice = new GetPatientsLuceneService(ConfigurationManager.AppSettings["PatientIndexPath"]);
            _recordsLuceneServiceservice = new GetRecordsLuceneService(ConfigurationManager.AppSettings["RecordIndexPath"]);
            _treatmentsLuceneServiceservice = new TreatmentsLuceneService(ConfigurationManager.AppSettings["TreatmentIndexPath"]);
        }

        public IPatient GetPatientBy(string cpf)
        {
            var patient = _patientsLuceneServiceservice.GetPatientBy(cpf);

            patient = SetHospitalFrom(patient);
            patient.Records = _recordsLuceneServiceservice.GetRecordBy(cpf);

            var treatments = _treatmentsLuceneServiceservice.GetTreatments(patient.Records);

            if (treatments != null)
                patient.AddTreatments(treatments);

            return patient;
        }

        public IList<IPatient> GetPatientBy(IPatient patient)
        {
            var patients = _patientsLuceneServiceservice.GetPatients(patient.Name);
            return SetHospitalInPatientFrom(patients);
        }

        public IList<IPatient> AdvancedGetPatientBy(IPatient patient, List<string> hospitals)
        {
            var patients = _patientsLuceneServiceservice.GetPatientsAdvancedSearch(patient, hospitals);
            return SetHospitalInPatientFrom(patients);
        }

        private List<IPatient> SetHospitalInPatientFrom(List<IPatient> patients)
        {
            var patientList = new List<IPatient>();

            foreach (IPatient current in patients)
            {
                current.Hospital = FindBy(_hospitals, current.Hospital.Key);
                patientList.Add(current);
            }
            return patientList;
        }

        private IPatient SetHospitalFrom(IPatient current)
        {
            current.Hospital = FindBy(_hospitals, current.Hospital.Key);
            return current;
        }

        private Hospital FindBy(List<Hospital> hospitals, string key)
        {
            return hospitals.Find(x => x.Key == key);
        }
    }
}
