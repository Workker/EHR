using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Sumario;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class CidMap : ClassMap<Cid>
    {
        public CidMap()
        {
            Id(c => c.Id);
            Map(c => c.Descricao);
            Map(c => c.CodigoCid);
        }
    }
}
