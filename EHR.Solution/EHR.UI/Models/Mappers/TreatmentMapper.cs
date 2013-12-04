using AutoMapper;
using EHR.CoreShared.Interfaces;

namespace EHR.UI.Models.Mappers
{
    public static class TreatmentMapper
    {
        public static TreatmentModel MapTreatmentModelFrom(ITreatment treatment)
        {
            Mapper.CreateMap<ITreatment, TreatmentModel>().ForMember(hosp => hosp.Hospital, source => source.Ignore());
            var treatmentModel = Mapper.Map<ITreatment, TreatmentModel>(treatment);
            treatmentModel.Hospital = HospitalMapper.MapHospitalModelFrom(treatment.Hospital);
            return treatmentModel;
        }
    }
}