using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class HighDataMap : ClassMap<HighData>
    {
        HighDataMap()
        {
            Id(h => h.Id);
            Map(h => h.HighType).CustomType<short>();
            Map(h => h.ConditionOfThePatientAtHigh).CustomType<short>();
            Map(h => h.DestinationOfThePatientAtDischarge).CustomType<short>();
            Map(h => h.OrientationOfMultidisciplinaryTeamsMet).CustomType<short>();
            Map(h => h.TermMedicalReviewAt);
            References(h => h.Specialty).Cascade.None();
            Map(h => h.PrescribedHigh);
            Map(h => h.PersonWhoDeliveredTheSummary);
            Map(h => h.DeliveredDate);
            HasMany(s => s.ComplementaryExams).Cascade.AllDeleteOrphan();
        }
    }
}