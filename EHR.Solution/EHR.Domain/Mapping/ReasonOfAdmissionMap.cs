using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ReasonOfAdmissionMap : ClassMap<ReasonOfAdmission>
    {
        public ReasonOfAdmissionMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
            
        }
    }
}
