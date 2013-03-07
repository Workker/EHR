using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Migracao;
using EHR.Domain.Entities.Interfaces;
using FluentNHibernate.Mapping;

namespace EHR.Domain.MappingMigracao
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
