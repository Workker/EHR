using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ProfessionalRegistrationMap: ClassMap<ProfessionalRegistration>
    {
        ProfessionalRegistrationMap()
        {
            Id(p => p.Id);
            Map(p => p.Number);
            Map(p => p.Type).CustomType<short>();
            References(p => p.State).Cascade.None();
        }
    }
}
