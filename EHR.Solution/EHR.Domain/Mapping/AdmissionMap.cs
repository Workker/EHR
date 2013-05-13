﻿using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public sealed class AdmissionMap : ClassMap<Admission>
    {
        public AdmissionMap()
        {
            Id(x => x.Id);
            HasMany(x => x.ReasonOfAdmission).Cascade.All();
        }
    }
}
