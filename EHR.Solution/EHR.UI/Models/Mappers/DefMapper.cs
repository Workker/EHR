using AutoMapper;
using EHR.CoreShared;
using EHR.Domain.Entities;
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

        //public static DefModel MapDefModelFrom(DEF def)
        //{
        //    Mapper.CreateMap<DEF, DefModel>();
        //    return Mapper.Map<DEF, DefModel>(def);
        //}

        public static DefModel MapDefModelFrom(DEF def)
        {
            Mapper.CreateMap<DEF, DefModel>();
            var defModel = Mapper.Map<DEF, DefModel>(def);
            defModel.Code = def.Id;
            return defModel;
        }
    }
}