using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Tus : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string MedicalProcedure { get; set; }
    }
}

