using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EHR.UI.Models.Mappers
{
    public static class SummaryMapper
    {
        public static SummaryModel MapSummaryModelFrom(Summary summary)
        {
            Mapper.CreateMap<Summary, SummaryModel>()
                .ForMember(hosp => hosp.Hospital, source => source.Ignore())
                .ForMember(al => al.Allergies, so => so.Ignore())
                .ForMember(di => di.Diagnostics, so => so.Ignore())
                .ForMember(proc => proc.Procedures, so => so.Ignore())
                .ForMember(hemo => hemo.Hemotransfusions, so => so.Ignore())
                .ForMember(ex => ex.Exams, so => so.Ignore())
                .ForMember(me => me.Medications, so => so.Ignore())
                .ForMember(hd => hd.DischargeData, so => so.Ignore())
                .ForMember(p => p.Patient, so => so.Ignore())
                .ForMember(ac => ac.Actions, souce => souce.Ignore())
                .ForMember(vi => vi.Views, source => source.Ignore())
                .ForMember(tr => tr.Treatment, source => source.Ignore());

            var summaryModel = Mapper.Map<Summary, SummaryModel>(summary);

            var views = new List<HistoryRecord>();
            var actions = new List<HistoryRecord>();

            foreach (var historyRecord in summary.History)
            {
                if (historyRecord.Action.Id == 4)
                {
                    views.Add(historyRecord);
                }
                else
                {
                    actions.Add(historyRecord);
                }
            }

            summaryModel.DischargeData = DischargeDataMapper.MapHighDataModelFrom(summary);
            summaryModel.Allergies = AllergyMapper.MapAllergyModelsFrom(summary.Allergies);
            summaryModel.Diagnostics = DiagnosticMapper.MapDiagnosticsModelsFrom(summary.Diagnostics);
            summaryModel.Procedures = ProcedureMapper.MapProceduresModelsFrom(summary.Procedures);
            summaryModel.Medications = MedicationMapper.MapMedicationModelsFrom(summary.Medications);
            summaryModel.Hemotransfusions = HemotransfusionMapper.MapHemotransfusionModelsFrom(summary.Hemotransfusions);
            summaryModel.Exams = ExamMapper.MapExamModelsFrom(summary.Exams);
            summaryModel.Patient = PatientMapper.MapPatientModelFrom(summary.Patient);
            summaryModel.Hospital = HospitalMapper.MapHospitalModelFrom(summary.Hospital);
            summaryModel.Actions = HistoryRecordMapper.MapHistoryRecordModelsFrom(actions.OrderByDescending(hr => hr.Date).Take(10));
            summaryModel.Views = HistoryRecordMapper.MapHistoryRecordModelsFrom(
                views.OrderByDescending(hr => hr.Date).GroupBy(x => x.Account.Id).Select(x => x.FirstOrDefault()).Take(10).ToList());
            summaryModel.Treatment = TreatmentMapper.MapTreatmentModelFrom(summary.Treatment);

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
    }
}