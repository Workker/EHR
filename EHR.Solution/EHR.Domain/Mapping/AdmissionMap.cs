﻿using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public sealed class AdmissionMap : ClassMap<Admission>
    {
        public AdmissionMap()
        {
            Id(x => x.Id);
            Map(x => x.ReasonOfAdmission).CustomType<int>();
        }
    }
}
