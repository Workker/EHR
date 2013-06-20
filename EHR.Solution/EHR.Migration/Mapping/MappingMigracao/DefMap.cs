using EHR.Migration.Entities.Migracao;
using FluentNHibernate.Mapping;

namespace EHR.Migration.Mapping.MappingMigracao
{
    public class DefMigracaoMap : ClassMap<DefMigracao>
    {
        public DefMigracaoMap() 
        {
            Id(d => d.Id, "SEQ_MED");
            Map(d => d.Produto, "PRODUTO");
            Table("TBDEF");
        }
    }
}
