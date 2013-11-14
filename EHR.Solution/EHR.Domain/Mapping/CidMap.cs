using EHR.CoreShared.Entities;

namespace EHR.Domain.Mapping
{
    public class CidMap : ValueObjectMap<CID>
    {
        public CidMap()
        {
            Id(c => c.Id).GeneratedBy.Identity();
            Map(c => c.Code);
            Map(c => c.AbbreviatedDescription);
        }
    }
}
