﻿using EHR.Domain.Entities;
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