using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class AllergyTypeMap : ClassMap<AllergyType>
    {
        public AllergyTypeMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
        }
    }
}
