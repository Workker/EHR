using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class MedicalReviewMap : ClassMap<MedicalReview>
    {
        MedicalReviewMap()
        {
            Id(m => m.Id);
            Map(m => m.TermMedicalReviewAt);
            References(m => m.Specialty).Cascade.None();
        }
    }
}
