using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ProcedureMap : ClassMap<Procedure>
    {
        public ProcedureMap()
        {
            Id(x => x.Id);
            Map(x => x.Tus);
            Map(x => x.Date);
        }
    }
}
