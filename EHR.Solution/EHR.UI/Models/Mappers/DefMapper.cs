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

    public static class CuidadoMedicoMapper
    {
        public static List<CuidadoMedicoModel> MapCuidadoMedicoModelsFrom(IList<CuidadoMedico> cuidadosMedicos)
        {
            var defModels = new List<CuidadoMedicoModel>();
            foreach (var cuidadoMedico in cuidadosMedicos)
            {
                var cuidadoMedicoModel = MapCuidadoMedicoModelFrom(cuidadoMedico);
                cuidadoMedicoModel.Code = cuidadoMedico.Id;
                defModels.Add(cuidadoMedicoModel);
            }
            return defModels;
        }

        public static CuidadoMedicoModel MapCuidadoMedicoModelFrom(CuidadoMedico def)
        {
            Mapper.CreateMap<CuidadoMedico, CuidadoMedicoModel>();
            var defModel = Mapper.Map<CuidadoMedico, CuidadoMedicoModel>(def);
            defModel.Code = def.Id;
            return defModel;
        }
    }
}