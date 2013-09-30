using EHR.CoreShared;

namespace EHR.Domain.Entities
{
    public class Medication
    {
        public virtual int Id { get; set; }
        public virtual MedicationTypeEnum Type { get; set; }
        public virtual DEF Def { get; set; }
        public virtual string Presentation { get; set; }
        public virtual PresentationTypeEnum PresentationType { get; set; }
        public virtual string Dose { get; set; }
        public virtual DosageEnum Dosage { get; set; }
        public virtual WayEnum Way { get; set; }
        public virtual string Place { get; set; }
        public virtual FrequencyEnum Frequency { get; set; }
        public virtual FrequencyCaseEnum FrequencyCase { get; set; }
        public virtual int Duration { get; set; }
        public virtual string Description { get; set; }
    }
}