using System.Collections.Generic;
using AutoMapper;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class ProfessionalRegistrationMapper
    {
        public static List<ProfessionalRegistrationModel> MapProfessionalRegistrationsModelFrom(IList<ProfessionalRegistration> professionalRegistrationObjs)
        {
            var professionalRegistrations = new List<ProfessionalRegistrationModel>();
            foreach (ProfessionalRegistration professionalRegistration in professionalRegistrationObjs)
            {
                professionalRegistrations.Add(MapProfessionalRegistrationModelFrom(professionalRegistration));
            }
            return professionalRegistrations;
        }

        public static ProfessionalRegistrationModel MapProfessionalRegistrationModelFrom(ProfessionalRegistration professionalRegistrationObj)
        {
            Mapper.CreateMap<ProfessionalRegistration, ProfessionalRegistrationModel>().ForMember(dest => dest.State, source => source.Ignore());

            var professionalRegistration = Mapper.Map<ProfessionalRegistration, ProfessionalRegistrationModel>(professionalRegistrationObj);

            professionalRegistration.State = StateMapper.MapSpecialtyModelFrom(professionalRegistrationObj.State);

            return professionalRegistration;
        }

    }
}