using AutoMapper;
using EHR.Domain.Entities;
using EHR.UI.Models;
using System.Collections.Generic;

namespace EHR.UI.Mappers
{
    public static class SummaryMapper
    {
        public static SummaryModel MapSummaryModelFrom(Summary summary)
        {
            Mapper.CreateMap<Summary, SummaryModel>().ForMember(hosp => hosp.Hospital, source => source.Ignore())
                .ForMember(al => al.Allergies, so => so.Ignore()).ForMember(di => di.Diagnostics, so => so.Ignore())
                .ForMember(proc => proc.Procedures, so => so.Ignore()).ForMember(hemo => hemo.Hemotransfusions, so => so.Ignore())
                .ForMember(ex => ex.Exams, so => so.Ignore()).ForMember(me => me.Medications, so => so.Ignore())
                .ForMember(hd => hd.HighData, so => so.Ignore()).ForMember(ac => ac.LastVisitors, so => so.Ignore())
                .ForMember(p => p.Patient, so => so.Ignore());

            var summaryModel = Mapper.Map<Summary, SummaryModel>(summary);
            summaryModel.HighData = HighDataMapper.MapHighDataModelFrom(summary.HighData);
            summaryModel.Allergies = AllergyMapper.MapAllergyModelsFrom(summary.Allergies);
            summaryModel.Diagnostics = DiagnosticMapper.MapDiagnosticsModelsFrom(summary.Diagnostics);
            summaryModel.Procedures = ProcedureMapper.MapProceduresModelsFrom(summary.Procedures);
            summaryModel.Medications = MedicationMapper.MapMedicationModelsFrom(summary.Medications);
            summaryModel.Hemotransfusions = HemotransfusionMapper.MapHemotransfusionModelsFrom(summary.Hemotransfusions);
            summaryModel.Exams = ExamMapper.MapExamModelsFrom(summary.Exams);
            summaryModel.LastVisitors = MapViewModelFrom(summary.Views);
            summaryModel.Patient = PatientMapper.MapPatientModelFrom(summary.Patient);
            summaryModel.Hospital = HospitalMapper.MapHospitalModelFrom(summary.Hospital);

            return summaryModel;
        }

        public static List<SummaryModel> MapSummaryModelFrom(IEnumerable<Summary> summaries)
        {
            var sumaryModels = new List<SummaryModel>();

            foreach (Summary summary in summaries)
            {
                var summaryModel = MapSummaryModelFrom(summary);
                sumaryModels.Add(summaryModel);
            }
            return sumaryModels;
        }

        private static IList<ViewModel> MapViewModelFrom(IList<View> views)
        {
            var viewModels = new List<ViewModel>();

            foreach (var view in views)
            {
                Mapper.CreateMap<View, ViewModel>();
                viewModels.Add(new ViewModel()
                {
                    Id = view.Id,
                    Account = AccountMapper.MapAccountModelFrom(view.Account),
                    VisiteDate = view.VisiteDate
                });
            }

            return viewModels;
        }

    }
}