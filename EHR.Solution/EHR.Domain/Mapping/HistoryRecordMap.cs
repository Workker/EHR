using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class HistoryRecordMap : ClassMap<HistoryRecord>
    {
        HistoryRecordMap()
        {
            Id(h => h.Id);
            References(h => h.Account).Cascade.None();
            References(h => h.Action).Cascade.None();
            Map(h => h.Date).Column("DateOfAction");
        }
    }
}
