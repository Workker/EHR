using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class HemotransfusionMap : ClassMap<Hemotransfusion>
    {
        public HemotransfusionMap()
        {
            Id(x => x.Id);
            Map(x => x.Type).CustomType<int>();
            HasMany(x => x.Reactions).Cascade.All();

        }
    }
}
