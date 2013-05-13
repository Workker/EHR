using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class TusMap : ValueObjectMap<Tus>
    {
        public TusMap()
        {
            Map(p => p.Code);
        }
    }
}
