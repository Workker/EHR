using EHR.Domain.Entities;

namespace EHR.Domain.Mapping
{
    public class SpecialtyMap : ValueObjectMap<Specialty>
    {
        public SpecialtyMap()
        {
            Id(s => s.Id).GeneratedBy.Identity();
            Map(s => s.CodeTerm);
        }
    }
}
