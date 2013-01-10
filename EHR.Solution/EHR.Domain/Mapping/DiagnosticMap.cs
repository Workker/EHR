using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Patient;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public sealed class DiagnosticMap : ClassMap<Diagnostic>
    {
        public DiagnosticMap()
        {
            Id(x => x.Id);
            Map(x => x.Type);
            Map(x => x.CidCode);
            Map(x => x.Cid);
        }
    }
}
