using AutoMapper;
using EHR.Domain.Entities;
using EHR.UI.Models;

namespace EHR.UI.Mappers
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