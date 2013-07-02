using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EHR.Domain.Entities;
using Legacy = ImportLegacySummary.DTO;

namespace ImportLegacySummary.Mapping
{
    public class SummaryMap : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Legacy.Summary, Summary>()
                .ForMember(d => d.Date, o => o.MapFrom(m => DateTime.Now))
                .ForMember(d => d.Admission, o => o.MapFrom(m =>
                    new Admission
                    {
                        ReasonOfAdmission = new List<ReasonOfAdmission> { 
                            new ReasonOfAdmission { Description = m.Admission } 
                        }
                    }
                ))
                .AfterMap(AddDiagnostics)
                .AfterMap(AddProcedures)
                .AfterMap(AddMedications)
                .AfterMap(AddHemotransfusions);
        }

        private void AddDiagnostics(Legacy.Summary legacySummary, Summary newSummary) 
        {
            foreach (var legacyDiagnostic in legacySummary.Diagnostics)
            {
                if (legacyDiagnostic != null && !String.IsNullOrWhiteSpace(legacyDiagnostic.Type))
                {
                    short diagnosticTypeId = Convert.ToInt16(legacyDiagnostic.Type);
                    Cid cid = new Cid { Code = legacyDiagnostic.Cid };

                    newSummary.CreateDiagnostic(new DiagnosticType { Id = diagnosticTypeId }, cid);
                }
            }
        }

        private void AddProcedures(Legacy.Summary legacySummary, Summary newSummary) 
        {
            foreach (var legacyProcedure in legacySummary.Procedures)
            {
                if (legacyProcedure != null 
                    && legacyProcedure.DateProc.HasValue
                    && !String.IsNullOrWhiteSpace(legacyProcedure.TusCode))
                {
                    DateTime procDate = legacyProcedure.DateProc.Value;
                    Tus tus = new Tus { Code = legacyProcedure.TusCode };

                    newSummary.CreateProcedure(procDate.Month, procDate.Day, procDate.Year, tus);
                }
            }
        }

        private void AddMedications(Legacy.Summary legacySummary, Summary newSummary) 
        {
            foreach (var legacyMedication in legacySummary.Medications)
            {
                if (legacyMedication != null) 
                {
                    // TODO newSummary.CreateMedication();
                }
            }
        }

        private void AddHemotransfusions(Legacy.Summary legacySummary, Summary newSummary) 
        {
            foreach (var legacyHemotransfusion in legacySummary.Hemotransfusions)
            {
                if (legacyHemotransfusion != null)
                {
                    // TODO newSummary.CreateHemotransfusion();
                }
            }
        }
    }
}
