using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public sealed class AllergyMap : ClassMap<Allergy>
    {
        public AllergyMap()
        {
            Id(x => x.Id);
            Map(x => x.TheWhich);
            Map(x => x.Type).CustomType<int>();
        }
    }
}
