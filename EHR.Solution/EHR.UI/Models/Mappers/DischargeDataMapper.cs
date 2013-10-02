using AutoMapper;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class DischargeDataMapper
    {
        public static DischargeDataModel MapHighDataModelFrom(Summary summary)
        {
            var dischargeData = summary.HighData;

            Mapper.CreateMap<DischargeData, DischargeDataModel>().ForMember(ec => ec.ComplementaryExams, source => source.Ignore())
                .ForMember(mr => mr.MedicalReviews, source => source.Ignore()).ForMember(cd => cd.ConditionAtDischarge, source => source.Ignore());

            var dischargeDataModel = Mapper.Map<DischargeData, DischargeDataModel>(dischargeData);

            dischargeDataModel.ConditionAtDischarge = dischargeData.ConditionAtDischarge == null ? short.MinValue : dischargeData.ConditionAtDischarge.Id;
            dischargeDataModel.DestinationOfThePatientAtDischarge = (short)dischargeData.DestinationAtDischarge;
            dischargeDataModel.MultidisciplinaryTeamsMet = (short)dischargeData.MultidisciplinaryTeamsMet;

            dischargeDataModel.PrescribedHighYear = summary.Date.Value.Year == null ? 0 : summary.Date.Value.Year;
            dischargeDataModel.PrescribedHighMonth = summary.Date.Value.Month == null ? 0 : summary.Date.Value.Month;
            dischargeDataModel.PrescribedHighDay = summary.Date.Value.Day == null ? 0 : summary.Date.Value.Day;

            dischargeDataModel.PrescribedHighHour = summary.Date.Value.Hour == null ? 0 : summary.Date.Value.Hour;
            dischargeDataModel.PrescribedHighMinute = summary.Date.Value.Minute == null ? 0 : summary.Date.Value.Minute;
            
            dischargeDataModel.DeliveredDateYear = dischargeData.Date == null ? 0 : dischargeData.Date.Value.Day;
            dischargeDataModel.DeliveredDateMonth = dischargeData.Date == null ? 0 : dischargeData.Date.Value.Month;
            dischargeDataModel.DeliveredDateDay = dischargeData.Date == null ? 0 : dischargeData.Date.Value.Day;

            foreach (var medicalReview in dischargeData.MedicalReviews)
            {
                dischargeDataModel.MedicalReviews.Add(MedicalReviewMapper.MapmedicalReviewModelFrom(medicalReview));
            }

            foreach (var complementaryExam in dischargeData.ComplementaryExams)
            {
                dischargeDataModel.ComplementaryExams.Add(ComplementaryExamMapper.MapComplementaryExamModelFrom(complementaryExam));
            }

            return dischargeDataModel;
        }
    }
}