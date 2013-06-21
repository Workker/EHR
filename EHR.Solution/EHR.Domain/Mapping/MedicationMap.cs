using EHR.Domain.Entities;
using FluentNHibernate.Mapping;

namespace EHR.Domain.Mapping
{
    public class MedicationMap : ClassMap<Medication>
    {
        MedicationMap()
        {
            Id(m => m.Id);
            Map(m => m.Type).CustomType<short>();
            References(m => m.Def).Cascade.None();
            Map(m => m.Presentation);
            Map(m => m.PresentationType);
            Map(m => m.Dose);
            Map(m => m.Dosage);
            Map(m => m.Way);
            Map(m => m.Place);
            Map(m => m.Frequency);
            Map(m => m.FrequencyCase);
            Map(m => m.Duration);
        }
    }
}