using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Patient;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public sealed class AdmissionMap : ClassMap<Admission>
    {
        public AdmissionMap()
        {
            Id(x => x.Id);
            Map(x => x.ReasonOfAdmission);
        }
    }
}
