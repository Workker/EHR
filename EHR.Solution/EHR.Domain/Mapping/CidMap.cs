using EHR.CoreShared;
using EHR.Domain.Entities;

namespace EHR.Domain.Mapping
{
    public class CidMap : ValueObjectMap<CID>
    {
        public CidMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            Map(c => c.Code);
        }
    }
}
