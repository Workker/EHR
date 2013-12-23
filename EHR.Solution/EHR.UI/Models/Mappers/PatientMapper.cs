using AutoMapper;
using EHR.CoreShared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EHR.UI.Models.Mappers
{
    public static class PatientMapper
    {
        public static PatientModel MapPatientModelFrom(IPatient patient, string treatmentStr)
        {
            Mapper.CreateMap<IPatient, PatientModel>().ForMember(dest => dest.Treatments, source => source.Ignore());

            var patientModel = Mapper.Map<IPatient, PatientModel>(patient);
            var treatmentModels = new List<TreatmentModel>();

            patientModel.Hospital = HospitalMapper.MapHospitalModelFrom(patient.Hospital);

            //AddHospital(patient, treatmentStr, patientModel);

            foreach (var treatment in patient.Treatments)
            {
                var treatmentModel = TreatmentMapper.MapTreatmentModelFrom(treatment);
                treatmentModels.Add(treatmentModel);
            }

            patientModel.Treatments = treatmentModels;

            return patientModel;
        }

        public static PatientModel MapPatientModelFrom(IPatient patient)
        {
            Mapper.CreateMap<IPatient, PatientModel>().ForMember(dest => dest.Treatments, source => source.Ignore());

            var patientModel = Mapper.Map<IPatient, PatientModel>(patient);

            return patientModel;
        }

        public static IEnumerable<PatientModel> MapPatientModelFrom(IEnumerable<IPatient> patients)
        {
            Mapper.CreateMap<IPatient, PatientModel>().ForMember(dest => dest.Hospital, source => source.Ignore());

            var patientModels = new List<PatientModel>();

            foreach (var item in patients)
            {
                var account = Mapper.Map<IPatient, PatientModel>(item);
                account.Hospital = HospitalMapper.MapHospitalModelFrom(item.Hospital);
                patientModels.Add(account);
            }
            return patientModels;
        }

        //private static void AddHospital(IPatient patient, string treatmentStr, PatientModel patientModel)
        //{
        //    if (patient.Treatments != null && patient.Treatments.Count > 0 && !string.IsNullOrEmpty(treatmentStr) &&
        //        patient.Treatments.Count(t => t.Id == treatmentStr) > 0)
        //    {
        //        var firstOrDefault = patient.Treatments.FirstOrDefault(t => t.Id == treatmentStr);
        //        if (firstOrDefault != null)
        //            patientModel.Hospital = HospitalMapper.MapHospitalModelFrom(firstOrDefault.Hospital);
        //    }
        //    else
        //        patientModel.Hospital = HospitalMapper.MapHospitalModelFrom(patient.Hospital);
        //}
    }
}