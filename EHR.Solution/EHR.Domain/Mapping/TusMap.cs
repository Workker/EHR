using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class TusMap : ValueObjectMap<Tus>
    {
        public TusMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            Map(p => p.Code);
        }
    }
}
