using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities;
using EHR.Domain.Entities.Interfaces;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public abstract class ValueObjectMap<T> : ClassMap<T> where T : ValueObject
    {
        public ValueObjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
        }
    }
}
