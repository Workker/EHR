using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Patient;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public sealed class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Id(x => x.Id);
            References(x => x.Admission);
            HasMany(x => x.Allergies).KeyNullable();
            HasMany(x => x.Diagnostics);
            Map(x => x.MedicinesOfUsePrior);
            Map(x => x.Annotations);
        }
    }
}
