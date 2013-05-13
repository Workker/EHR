using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class TusMap : ClassMap<Tus>
    {
        public TusMap()
        {
            Id(p => p.Id);
            Map(p => p.Procedure);
            Map(p => p.Code);
        }
    }
}
