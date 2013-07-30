using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class SpecialtyMapper
    {
        public static List<SpecialtyModel> MapSpecialtyModelsFrom(IList<Specialty> specialties)
        {
            var specialtyModels = new List<SpecialtyModel>();
            foreach (var specialty in specialties)
            {
                var specialtyModel = MapSpecialtyModelFrom(specialty);
                specialtyModel.Code = specialty.Id;
                specialtyModels.Add(specialtyModel);
            }
            return specialtyModels;
        }

        public static SpecialtyModel MapSpecialtyModelFrom(Specialty specialty)
        {
            Mapper.CreateMap<Specialty, SpecialtyModel>();
            return Mapper.Map<Specialty, SpecialtyModel>(specialty);
        }
    }
}