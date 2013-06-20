using EHR.Migration.Entities.Migracao;
using FluentNHibernate.Mapping;

namespace EHR.Migration.Mapping.MappingMigracao
{
    public class ProcedimentoMigracaoMap : ClassMap<ProcedimentoMigracao>
    {
        public ProcedimentoMigracaoMap()
        {
            Table("TBINT106");
            Id(p => p.Id, "IDINT106");
            Map(p => p.Procedimento,"DSPROCED");
            Map(p => p.SubGrupo, "DSSUBGRP");
            Map(p => p.Grupo,"DSGRUPO");
            Map(p => p.CodigoProcedimento, "CDTUSS");
        }
    }
}
