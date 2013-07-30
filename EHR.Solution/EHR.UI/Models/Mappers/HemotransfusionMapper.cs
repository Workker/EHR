using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class HemotransfusionMapper
    {
        public static List<HemotransfusionModel> MapHemotransfusionModelsFrom(IList<Hemotransfusion> hemotransfusions)
        {
            var hemoModels = new List<HemotransfusionModel>();
            foreach (var hemotransfusion in hemotransfusions)
            {
                Mapper.CreateMap<Hemotransfusion, HemotransfusionModel>();
                var hemotransfusionModel = Mapper.Map<Hemotransfusion, HemotransfusionModel>(hemotransfusion);
                hemotransfusionModel.HemotransfusionType = hemotransfusion.Type.Id;
                foreach (var reaction in hemotransfusion.Reactions)
                {
                    hemotransfusionModel.ReactionTypes.Add(reaction.Id);
                }

                hemoModels.Add(hemotransfusionModel);
            }
            return hemoModels;
        }
    }
}