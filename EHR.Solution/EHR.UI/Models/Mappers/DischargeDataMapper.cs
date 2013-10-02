using AutoMapper;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class DischargeDataMapper
    {
        public static DischargeDataModel MapHighDataModelFrom(DischargeData dischargeData)
        {
            Mapper.CreateMap<DischargeData, DischargeDataModel>().ForMember(ec => ec.ComplementaryExams, source => source.Ignore())
                .ForMember(mr => mr.MedicalReviews, source => source.Ignore()).ForMember(cd => cd.ConditionAtDischarge, source => source.Ignore());

            var dischargeDataModel = Mapper.Map<DischargeData, DischargeDataModel>(dischargeData);

            dischargeDataModel.ConditionAtDischarge = dischargeData.ConditionAtDischarge == null ? short.MinValue : dischargeData.ConditionAtDischarge.Id;
            dischargeDataModel.DestinationOfThePatientAtDischarge = (short)dischargeData.DestinationAtDischarge;
            dischargeDataModel.MultidisciplinaryTeamsMet = (short)dischargeData.MultidisciplinaryTeamsMet;
            dischargeDataModel.PrescribedHighYear = dischargeData.PrescribedHigh == null ? 0 : dischargeData.PrescribedHigh.Value.Year;
            dischargeDataModel.PrescribedHighMonth = dischargeData.PrescribedHigh == null ? 0 : dischargeData.PrescribedHigh.Value.Month;
            dischargeDataModel.PrescribedHighDay = dischargeData.PrescribedHigh == null ? 0 : dischargeData.PrescribedHigh.Value.Day;
            dischargeDataModel.DeliveredDateYear = dischargeData.Date == null ? 0 : dischargeData.Date.Value.Year;
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