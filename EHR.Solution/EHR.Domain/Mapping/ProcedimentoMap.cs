using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Sumario;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ProcedimentoMap : ClassMap<Procedimento>
    {
        public ProcedimentoMap()
        {
            Id(p => p.Id);
            Map(p => p.NomeProcedimento);
            Map(p => p.SubGrupo);
            Map(p => p.Grupo);
            Map(p => p.CodigoProcedimento);
        }
    }
}
