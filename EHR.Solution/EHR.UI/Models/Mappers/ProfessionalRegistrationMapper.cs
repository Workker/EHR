using AutoMapper;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class ProfessionalRegistrationMapper
    {
        public static ProfessionalRegistrationModel MapProfessionalRegistrationModelFrom(ProfessionalRegistration professionalRegistrationObj)
        {
            Mapper.CreateMap<ProfessionalRegistration, ProfessionalRegistrationModel>();

            return Mapper.Map<ProfessionalRegistration, ProfessionalRegistrationModel>(professionalRegistrationObj);
        }

    }
}