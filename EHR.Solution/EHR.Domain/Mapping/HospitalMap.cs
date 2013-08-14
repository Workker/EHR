using EHR.CoreShared;

namespace EHR.Domain.Mapping
{
    public class HospitalMap : ValueObjectMap<Hospital>
    {
        HospitalMap()
        {
            Id(h => h.Id).GeneratedBy.Identity();
            Map(h => h.Name);
            Map(h => h.URLImage);
        }
    }
}