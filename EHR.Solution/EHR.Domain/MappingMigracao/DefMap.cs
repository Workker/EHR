using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Migracao;
using EHR.Domain.Entities.Interfaces;
using FluentNHibernate.Mapping;

namespace EHR.Domain.MappingMigracao
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
