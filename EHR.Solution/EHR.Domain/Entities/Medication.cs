using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Medication : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual MedicationTypeEnum Type { get; set; }
        public virtual Def Def { get; set; }
        public virtual int Duration { get; set; }
    }
}
