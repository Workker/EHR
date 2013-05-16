using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class SummaryMap : ClassMap<Summary>
    {
        public SummaryMap()
        {
            Id(s => s.Id);
            Map(s => s.Cpf);
            Map(s => s.Date);
            Map(s => s.Observation);
            References(s => s.Admission);
            HasMany(s => s.Diagnostics).Cascade.AllDeleteOrphan();
            HasMany(s => s.Allergies).Cascade.AllDeleteOrphan();
            HasMany(s => s.Hemotransfusions).Cascade.AllDeleteOrphan();
            HasMany(s => s.Procedures).Cascade.AllDeleteOrphan();
        }
    }
}
