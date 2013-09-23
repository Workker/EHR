using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class MedicationMap : ClassMap<Medication>
    {
        MedicationMap()
        {
            Id(m => m.Id);
            Map(m => m.Type).CustomType<short>().Column("MedicationType");
            References(m => m.Def).Cascade.None();
            Map(m => m.Presentation);
            Map(m => m.PresentationType).CustomType<short>();
            Map(m => m.Dose);
            Map(m => m.Dosage).CustomType<short>();
            Map(m => m.Way).CustomType<short>();
            Map(m => m.Place);
            Map(m => m.Frequency).CustomType<short>();
            Map(m => m.FrequencyCase).CustomType<short>();
            Map(m => m.Duration);
            Map(m => m.Description);
        }
    }
}