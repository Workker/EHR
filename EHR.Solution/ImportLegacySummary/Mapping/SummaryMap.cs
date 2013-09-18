using AutoMapper;
using EHR.CoreShared;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System;
using System.Collections.Generic;
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
            var diagnosticTypesRepository = new Types<DiagnosticType>();

            foreach (var legacyDiagnostic in legacySummary.Diagnostics)
            {
                if (legacyDiagnostic != null && !String.IsNullOrWhiteSpace(legacyDiagnostic.Type))
                {
                    short diagnosticTypeId = Convert.ToInt16(legacyDiagnostic.Type);
                    DiagnosticType diagnosticType = diagnosticTypesRepository.Get(diagnosticTypeId);
                    CID cid = new CID { Code = legacyDiagnostic.Cid };

                    newSummary.CreateDiagnostic(diagnosticType, cid);
                }
            }
        }

        private void AddProcedures(Legacy.Summary legacySummary, Summary newSummary)
        {
            var tusRepository = new TUSSRepository();

            foreach (var legacyProcedure in legacySummary.Procedures)
            {
                if (legacyProcedure != null
                    && legacyProcedure.DateProc.HasValue
                    && !String.IsNullOrWhiteSpace(legacyProcedure.TusCode))
                {
                    DateTime procDate = legacyProcedure.DateProc.Value;
                    TUS tus = tusRepository.GetByCode(legacyProcedure.TusCode);

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
                    //newSummary.CreateMedication();
                }
            }
        }

        private void AddHemotransfusions(Legacy.Summary legacySummary, Summary newSummary)
        {
            var hemotransfusionTypesRepository = new Types<HemotransfusionType>();

            foreach (var legacyHemotransfusion in legacySummary.Hemotransfusions)
            {
                if (legacyHemotransfusion != null)
                {
                    short hemotransfusionTypeId = Convert.ToInt16(legacyHemotransfusion.HemotransfusionTypeId);
                    HemotransfusionType hemotransfusionType = hemotransfusionTypesRepository.Get(hemotransfusionTypeId);

                    //newSummary.CreateHemotransfusion(hemotransfusionType);
                }
            }
        }
    }
}
