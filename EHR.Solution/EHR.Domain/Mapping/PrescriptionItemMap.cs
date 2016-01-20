using EHR.Domain.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Mapping
{
    public class PrescriptionItemMap : ClassMap<PrescriptionItem>
    {
        public PrescriptionItemMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Description);
            Map(p => p.PrescriptionItemType).CustomType<short>();
            Map(p => p.ActivePrinciple);
            Map(p => p.code);

        }
    }
}
