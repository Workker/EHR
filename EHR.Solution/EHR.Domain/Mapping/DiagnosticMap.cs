using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class DiagnosticMap : ClassMap<Diagnostic>
    {
        public DiagnosticMap()
        {
            Id(x => x.Id);
            References(x => x.Type);
            References(x => x.Cid);
            Map(d => d.Description);
        }
    }
}
