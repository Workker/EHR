using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Diagnostic : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual DiagnosticType Type { get; set; }
        public virtual Cid Cid { get; set; }
    }
}
