using EHR.Domain.Entities;

namespace EHR.Domain.Mapping
{
    public class HospitalMap : ValueObjectMap<Hospital>
    {
        HospitalMap()
        {
            Id(h => h.Id).GeneratedBy.Identity();
            Map(h => h.URLImage);
        }
    }
}