using AutoMapper;
using EHR.CoreShared.Interfaces;

namespace EHR.UI.Models.Mappers
{
    public static class TreatmentMapper
    {
        public static TreatmentModel MapTreatmentModelFrom(ITreatment treatment)
        {
            Mapper.CreateMap<ITreatment, TreatmentModel>();
            return Mapper.Map<ITreatment, TreatmentModel>(treatment);
        }
    }
}