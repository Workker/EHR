using AutoMapper;
using EHR.CoreShared.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class DefMapper
    {
        public static List<DefModel> MapDefModelsFrom(IList<DEF> defs)
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

        public static DefModel MapDefModelFrom(DEF def)
        {
            Mapper.CreateMap<DEF, DefModel>();
            var defModel = Mapper.Map<DEF, DefModel>(def);
            defModel.Code = def.Id;
            return defModel;
        }
    }
}