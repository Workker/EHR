using EHR.Domain.Entities;

namespace EHR.Domain.Mapping
{
    public class SpecialtyMap : ValueObjectMap<Specialty>
    {
        public SpecialtyMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
        }
    }
}
