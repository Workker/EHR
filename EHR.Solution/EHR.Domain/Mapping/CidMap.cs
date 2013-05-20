using EHR.Domain.Entities;

namespace EHR.Domain.Mapping
{
    public class CidMap : ValueObjectMap<Cid>
    {
        public CidMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            Map(c => c.Code);
        }
    }
}
