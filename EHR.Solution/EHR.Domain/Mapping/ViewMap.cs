using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ViewMap : ClassMap<View>
    {
        public ViewMap()
        {
            Id(v => v.Id);
            Map(v => v.VisiteDate);
            References(v => v.Account).Cascade.None();
        }
    }
}
