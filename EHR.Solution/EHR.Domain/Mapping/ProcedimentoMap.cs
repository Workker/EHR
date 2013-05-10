using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class ProcedimentoMap : ClassMap<Procedure>
    {
        public ProcedimentoMap()
        {
            Id(p => p.Id);
            Map(p => p.Date);
            Map(p => p.Tus);
        }
    }
}
