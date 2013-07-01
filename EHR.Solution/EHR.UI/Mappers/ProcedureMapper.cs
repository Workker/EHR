using AutoMapper;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System.Collections.Generic;

namespace EHR.UI.Mappers
{
    public static class ProcedureMapper
    {
        public static List<ProcedureModel> MapProceduresModelsFrom(IList<Procedure> procedures)
        {
            var proceduresModels = new List<ProcedureModel>();
            foreach (var procedure in procedures)
            {
                Mapper.CreateMap<Procedure, ProcedureModel>();
                var procedureModel = Mapper.Map<Procedure, ProcedureModel>(procedure);
                proceduresModels.Add(procedureModel);
            }
            return proceduresModels;
        }
    }
}