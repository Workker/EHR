using EHR.Domain.Entities;

namespace EHR.Domain.Mapping
{
    public class CidMap : ValueObjectMap<Cid>
    {
        public CidMap()
        {
            Map(c => c.Code);
        }
    }
}
