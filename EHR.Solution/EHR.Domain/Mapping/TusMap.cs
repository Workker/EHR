using EHR.CoreShared;


namespace EHR.Domain.Mapping
{
    public class TusMap : ValueObjectMap<TUS>
    {
        public TusMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            Map(p => p.Code);
        }
    }
}
