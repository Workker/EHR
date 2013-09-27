using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public class MedicalReviewMapper
    {
        public static List<MedicalReviewModel> MapmedicalReviewModelsFrom(IList<MedicalReview> medicalReviews)
        {
            var medicalReviewsModel = new List<MedicalReviewModel>();
            foreach (var medicalReview in medicalReviews)
            {
                var medicalReviewModel = MapmedicalReviewModelFrom(medicalReview);
                medicalReviewsModel.Add(medicalReviewModel);
            }
            return medicalReviewsModel;
        }

        public static MedicalReviewModel MapmedicalReviewModelFrom(MedicalReview medicalReview)
        {
            Mapper.CreateMap<MedicalReview, MedicalReviewModel>().ForMember(sp => sp.Specialty, so => so.Ignore());

            var medicalReviewModel = Mapper.Map<MedicalReview, MedicalReviewModel>(medicalReview);

            if (medicalReview.Specialty != null)
            {
                medicalReviewModel.Specialty = SpecialtyMapper.MapSpecialtyModelFrom(medicalReview.Specialty);
            }

            return medicalReviewModel;
        }
    }
}