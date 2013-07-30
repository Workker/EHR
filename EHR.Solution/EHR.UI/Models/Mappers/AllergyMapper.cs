using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class AllergyMapper
    {
        public static List<AllergyModel> MapAllergyModelsFrom(IList<Allergy> allergies)
        {
            var allergyModels = new List<AllergyModel>();

            foreach (var allergy in allergies)
            {
                Mapper.CreateMap<Allergy, AllergyModel>().ForMember(type => type.Types, source => source.Ignore());
                var allergyModel = Mapper.Map<Allergy, AllergyModel>(allergy);

                foreach (var allergyType in allergy.Types)
                {
                    allergyModel.Types.Add(allergyType.Id);
                }
                allergyModels.Add(allergyModel);
            }
            return allergyModels;
        }
    }
}