using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class ProcedureMapper
    {
        public static List<ProcedureModel> MapProceduresModelsFrom(IList<Procedure> procedures)
        {
            var proceduresModels = new List<ProcedureModel>();
            foreach (var procedure in procedures)
            {
                var procedureModel = MapProcedureModel(procedure);
                proceduresModels.Add(procedureModel);
            }
            return proceduresModels;
        }

        private static ProcedureModel MapProcedureModel(Procedure procedure)
        {
            Mapper.CreateMap<Procedure, ProcedureModel>().ForMember(tuss => tuss.Code, proc => proc.Ignore());

            var procedureModel = Mapper.Map<Procedure, ProcedureModel>(procedure);

            if (procedure.Tus != null)
            {
                procedureModel.Code = procedure.GetCode();
            }

            return procedureModel;
        }
    }
}