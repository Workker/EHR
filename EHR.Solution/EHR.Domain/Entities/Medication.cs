using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Medication : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual MedicationTypeEnum Type { get; set; }
        public virtual Def Def { get; set; }
        public virtual string Presentation { get; set; }
        public virtual string PresentationType { get; set; }
        public virtual string Dose { get; set; }
        public virtual string Dosage { get; set; }
        public virtual string Way { get; set; }
        public virtual string Place { get; set; }
        public virtual string Frequency { get; set; }
        public virtual string FrequencyCase { get; set; }
        public virtual int Duration { get; set; }
    }
}
