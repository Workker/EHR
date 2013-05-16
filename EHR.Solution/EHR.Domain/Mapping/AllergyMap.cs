using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class AllergyMap : ClassMap<Allergy>
    {
        public AllergyMap()
        {
            Id(x => x.Id);
            Map(x => x.TheWhich);
            HasMany(x => x.Types).Cascade.All();
        }
    }
}
