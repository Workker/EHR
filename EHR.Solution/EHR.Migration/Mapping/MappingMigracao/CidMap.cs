using EHR.Migration.Entities.Migracao;
using FluentNHibernate.Mapping;

namespace EHR.Migration.Mapping.MappingMigracao
{
    public class CidMigracaoMap : ClassMap<CidMigracao>
    {
        public CidMigracaoMap() 
        {
            Table("tbdwd057");
            Id(c => c.Id, "IDDWD057");
            Map(c => c.Descricao, "DSCID ");
            Map(c => c.CodigoCid, "CDCID");
        }
    }
}
