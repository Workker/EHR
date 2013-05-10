using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities;
using EHRIntegracao.Domain.Domain;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public sealed class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Id(x => x.Id);
            //HasMany(x => x.Admissions);
            //HasMany(x => x.Allergies).Cascade.AllDeleteOrphan();
            //HasMany(x => x.Diagnostics).Cascade.AllDeleteOrphan();
            //Map(x => x.MedicinesOfUsePrior);
            //Map(x => x.Annotations);
        }
    }
}
