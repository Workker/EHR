using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ProfessionalRegistrationMap: ClassMap<ProfessionalRegistration>
    {
        ProfessionalRegistrationMap()
        {
            Id(p => p.Id);
            Map(p => p.Number).Column("RNumber");
            Map(p => p.Type).CustomType<short>().Column("RType");
            References(p => p.State).Cascade.None();
        }
    }
}
