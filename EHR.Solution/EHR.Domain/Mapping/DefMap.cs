using EHR.CoreShared.Entities;

namespace EHR.Domain.Mapping
{
    public class DefMap : ValueObjectMap<DEF>
    {
        public DefMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            Map(d => d.ActivePrinciple);
        }
    }
}
