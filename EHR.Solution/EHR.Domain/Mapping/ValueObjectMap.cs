﻿using FluentNHibernate.Mapping;
using EHR.CoreShared;

namespace EHR.Domain.Mapping
{
    public abstract class ValueObjectMap<T> : ClassMap<T> where T : ValueObject
    {
        protected ValueObjectMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
        }
    }
}