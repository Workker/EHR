using AutoMapper;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class HighDataMapper
    {
        public static HighDataModel MapHighDataModelFrom(DischargeData highData)
        {
            Mapper.CreateMap<DischargeData, HighDataModel>().ForMember(ec => ec.ComplementaryExams, source => source.Ignore())
                .ForMember(s => s.Specialty, source => source.Ignore());
            var highDataModel = Mapper.Map<DischargeData, HighDataModel>(highData);

            highDataModel.PrescribedHighYear = highData.PrescribedHigh == null ? 0 : highData.PrescribedHigh.Value.Year;
            highDataModel.PrescribedHighMonth = highData.PrescribedHigh == null ? 0 : highData.PrescribedHigh.Value.Month;
            highDataModel.PrescribedHighDay = highData.PrescribedHigh == null ? 0 : highData.PrescribedHigh.Value.Day;

            if (highData.Specialty != null)
            {
                highDataModel.Specialty = new SpecialtyModel
                {
                    Id = highData.Specialty.Id,
                    Description = highData.Specialty.Description,
                    Code = highData.Specialty.Id
                };

            }

            highDataModel.DeliveredDateYear = highData.DeliveredDate == null ? 0 : highData.DeliveredDate.Value.Year;
            highDataModel.DeliveredDateMonth = highData.DeliveredDate == null ? 0 : highData.DeliveredDate.Value.Month;
            highDataModel.DeliveredDateDay = highData.DeliveredDate == null ? 0 : highData.DeliveredDate.Value.Day;

            foreach (var complementaryExam in highData.ComplementaryExams)
            {
                highDataModel.ComplementaryExams.Add(ComplementaryExamMapper.MapComplementaryExamModelFrom(complementaryExam));
            }

            return highDataModel;
        }
    }
}