using AutoMapper;
using EHR.CoreShared;
using EHR.Domain.Util;
using EHR.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EHR.UI.Mappers
{
    public static class PatientMapper
    {
        public static PatientModel MapPatientModelFrom(IPatientDTO patient, string treatmentStr)
        {
            Mapper.CreateMap<IPatientDTO, PatientModel>().ForMember(dest => dest.Treatments, source => source.Ignore());

            var patientModel = Mapper.Map<IPatientDTO, PatientModel>(patient);
            var treatmentModels = new List<TreatmentModel>();

            AddHospital(patient, treatmentStr, patientModel);

            foreach (var treatment in patient.Treatments)
            {
                var treatmentModel = TreatmentMapper.MapTreatmentModelFrom(treatment);
                treatmentModels.Add(treatmentModel);
            }

            patientModel.Treatments = treatmentModels;

            return patientModel;
        }

        public static IEnumerable<PatientModel> MapPatientModelFrom(IEnumerable<IPatientDTO> patients)
        {
            Mapper.CreateMap<IPatientDTO, PatientModel>().ForMember(dest => dest.Hospital, source => source.Ignore());

            var patientModels = new List<PatientModel>();

            foreach (var item in patients)
            {
                var account = Mapper.Map<IPatientDTO, PatientModel>(item);
                account.Hospital = EnumUtil.GetDescriptionFromEnumValue((DbEnum)Enum.Parse(typeof(DbEnum), item.Hospital.ToString()));
                patientModels.Add(account);
            }
            return patientModels;
        }

        private static void AddHospital(IPatientDTO patient, string treatmentStr, PatientModel patientModel)
        {
            if (patient.Treatments != null && patient.Treatments.Count > 0 && !string.IsNullOrEmpty(treatmentStr) &&
                patient.Treatments.Count(t => t.Id == treatmentStr) > 0)
            {
                patientModel.Hospital =
                    EnumUtil.GetDescriptionFromEnumValue(
                        (DbEnum)
                        Enum.Parse(typeof(DbEnum),
                                   patient.Treatments.FirstOrDefault(t => t.Id == treatmentStr).Hospital.ToString()));
            }
            else
                patientModel.Hospital =
                    EnumUtil.GetDescriptionFromEnumValue((DbEnum)Enum.Parse(typeof(DbEnum), patient.Hospital.ToString()));
        }
    }
}