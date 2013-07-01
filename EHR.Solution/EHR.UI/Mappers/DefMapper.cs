using AutoMapper;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System.Collections.Generic;

namespace EHR.UI.Mappers
{
    public static class DefMapper
    {
        public static List<DefModel> MapDefModelsFrom(IList<DefDTO> defs)
        {
            var defModels = new List<DefModel>();
            foreach (var def in defs)
            {
                var defModel = MapDefModelFrom(def);
                defModel.Code = def.Id;
                defModels.Add(defModel);
            }
            return defModels;
        }

        public static DefModel MapDefModelFrom(DefDTO def)
        {
            Mapper.CreateMap<DefDTO, DefModel>();
            return Mapper.Map<DefDTO, DefModel>(def);
        }

        public static DefModel MapDefModelFrom(Def def)
        {
            Mapper.CreateMap<Def, DefModel>();
            var defModel = Mapper.Map<Def, DefModel>(def);
            defModel.Code = def.Id;
            return defModel;
        }
    }
}