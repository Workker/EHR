using AutoMapper;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System.Collections.Generic;

namespace EHR.UI.Mappers
{
    public static class DiagnosticMapper
    {
        public static List<DiagnosticModel> MapDiagnosticsModelsFrom(IList<Diagnostic> diagnostics)
        {
            var diagnosticsModels = new List<DiagnosticModel>();
            foreach (var diagnostic in diagnostics)
            {
                Mapper.CreateMap<Diagnostic, DiagnosticModel>().ForMember(type => type.Type, diag => diag.Ignore()).ForMember(cid => cid.Cid, diag => diag.Ignore());
                var diagnosticsModel = Mapper.Map<Diagnostic, DiagnosticModel>(diagnostic);
                diagnosticsModel.Type = diagnostic.Type.Id;
                diagnosticsModel.Cid = CidMapper.MapCidModelFrom(diagnostic.Cid);
                diagnosticsModels.Add(diagnosticsModel);
            }
            return diagnosticsModels;
        }
    }
}