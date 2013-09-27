using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class DischargeDataMap : ClassMap<DischargeData>
    {
        DischargeDataMap()
        {
            Id(d => d.Id);
            Map(h => h.HighType).CustomType<short>();
            References(h => h.ConditionAtDischarge).Cascade.None();
            Map(h => h.DestinationAtDischarge).CustomType<short>();
            Map(h => h.MultidisciplinaryTeamsMet).CustomType<short>();
            HasMany(h => h.MedicalReviews).Cascade.AllDeleteOrphan();
            HasMany(s => s.ComplementaryExams).Cascade.AllDeleteOrphan();
            Map(h => h.PrescribedHigh);
            Map(h => h.PersonWhoDeliveredTheSummary);
            Map(h => h.Date).Column("DeliveredDate");
        }
    }
}