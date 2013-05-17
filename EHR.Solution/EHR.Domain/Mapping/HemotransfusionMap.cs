using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class HemotransfusionMap : ClassMap<Hemotransfusion>
    {
        public HemotransfusionMap()
        {
            Id(x => x.Id);
            References(x => x.Type);
            HasManyToMany(x => x.Reactions);
        }
    }
}
