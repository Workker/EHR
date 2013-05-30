using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class HospitalMap : ClassMap<Hospital>
    {
        HospitalMap()
        {
            Id(h => h.Id).GeneratedBy.Identity();
            Map(h => h.Name);
            Map(h => h.URLImage);
        }
    }
}