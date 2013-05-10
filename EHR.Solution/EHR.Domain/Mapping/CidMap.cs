using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class CidMap : ClassMap<Cid>
    {
        public CidMap()
        {
            Id(c => c.Id);
            Map(c => c.Code);
            Map(c => c.Description);
        }
    }
}
