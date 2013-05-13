using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class DefMap : ValueObjectMap<Def>
    {
        public DefMap() 
        {
        }
    }
}
