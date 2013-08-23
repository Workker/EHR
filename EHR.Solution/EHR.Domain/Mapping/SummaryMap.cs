using EHR.Domain.Entities;
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
            Map(s => s.Observation);
            Map(s => s.CodeMedicalRecord);
            Map(s => s.Hospital);
            Map(s => s.EntryDateTreatment);
            Map(s => s.Mdr);
            References(s => s.Admission);
            References(s => s.HighData).Cascade.All();
            HasMany(s => s.Diagnostics).Cascade.AllDeleteOrphan();
            HasMany(s => s.Allergies).Cascade.AllDeleteOrphan();
            HasMany(s => s.Hemotransfusions).Cascade.AllDeleteOrphan();
            HasMany(s => s.Procedures).Cascade.AllDeleteOrphan();
            HasMany(s => s.Medications).Cascade.AllDeleteOrphan();
            HasMany(s => s.Exams).Cascade.AllDeleteOrphan();
            HasMany(s => s.Views).Cascade.AllDeleteOrphan();
            References(s => s.Account).Cascade.None();
        }
    }
}
