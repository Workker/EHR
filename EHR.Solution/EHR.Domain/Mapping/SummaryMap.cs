﻿using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class SummaryMap : ClassMap<Summary>
    {
        public SummaryMap()
        {
            Id(s => s.Id);
            Map(s => s.Cpf);
            Map(s => s.Date).Column("SummaryDate");
            Map(s => s.Observation).Length(1000);
            Map(s => s.CodeMedicalRecord);
            References(s => s.Hospital).Cascade.None();
            Map(s => s.EntryDateTreatment);
            Map(s => s.Mdr).Length(1000);
            References(s => s.HighData).Cascade.All();
            HasMany(s => s.Diagnostics).Cascade.AllDeleteOrphan();
            HasMany(s => s.Allergies).Cascade.AllDeleteOrphan();
            HasMany(s => s.Hemotransfusions).Cascade.AllDeleteOrphan();
            HasMany(s => s.Procedures).Cascade.AllDeleteOrphan();
            HasMany(s => s.Medications).Cascade.AllDeleteOrphan();
            HasMany(s => s.PrescriptionsForService).Cascade.AllDeleteOrphan();
            HasMany(s => s.Exams).Cascade.AllDeleteOrphan();
            References(s => s.Account).Cascade.None();
            Map(s => s.Finalized);
            HasMany(s => s.History).Cascade.All().LazyLoad();
            Map(s => s.TreatmentId);
            HasManyToMany(s => s.ReasonOfAdmission).Cascade.None();
        }
    }
}
