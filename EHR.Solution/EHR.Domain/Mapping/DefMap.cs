using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Sumario;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class DefMap : ClassMap<Def>
    {
        public DefMap() 
        {
            Id(d => d.Id);
            Map(d => d.Produto);
        }
    }
}
