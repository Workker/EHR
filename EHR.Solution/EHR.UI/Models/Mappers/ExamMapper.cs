using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public static class ExamMapper
    {
        public static ExamModel MapExamModelFrom(Exam exam)
        {
            Mapper.CreateMap<Exam, ExamModel>();
            return Mapper.Map<Exam, ExamModel>(exam);
        }

        public static List<ExamModel> MapExamModelsFrom(IList<Exam> exams)
        {
            var examModels = new List<ExamModel>();
            foreach (var exam in exams)
            {
                examModels.Add(MapExamModelFrom(exam));
            }
            return examModels;
        }
    }
}