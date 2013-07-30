using AutoMapper;
using EHR.Domain.Entities;

namespace EHR.UI.Models.Mappers
{
    public static class ComplementaryExamMapper
    {
        public static ComplementaryExamModel MapComplementaryExamModelFrom(ComplementaryExam complementaryExam)
        {
            Mapper.CreateMap<ComplementaryExam, ComplementaryExamModel>();
            return Mapper.Map<ComplementaryExam, ComplementaryExamModel>(complementaryExam);
        }
    }
}