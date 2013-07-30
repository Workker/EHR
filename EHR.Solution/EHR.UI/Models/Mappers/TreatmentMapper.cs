using AutoMapper;
using EHR.CoreShared;

namespace EHR.UI.Models.Mappers
{
    public static class TreatmentMapper
    {
        public static TreatmentModel MapTreatmentModelFrom(ITreatmentDTO treatment)
        {
            Mapper.CreateMap<ITreatmentDTO, TreatmentModel>();
            return Mapper.Map<ITreatmentDTO, TreatmentModel>(treatment);
        }
    }
}