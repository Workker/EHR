using EHR.CoreShared;


namespace EHR.Domain.Mapping
{
    public class TusMap : ValueObjectMap<TUSS>
    {
        public TusMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            Map(p => p.Code);
        }
    }
}
