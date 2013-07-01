using AutoMapper;
using EHR.CoreShared;
using EHR.UI.Models;

namespace EHR.UI.Mappers
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