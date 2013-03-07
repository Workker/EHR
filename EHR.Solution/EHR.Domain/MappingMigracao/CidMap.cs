using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Migracao;
using EHR.Domain.Entities.Interfaces;
using FluentNHibernate.Mapping;

namespace EHR.Domain.MappingMigracao
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
