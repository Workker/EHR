using EHR.Domain.Entities.Interfaces;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Diagnostic : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual DiagnosticType Type { get; set; }
        public virtual Cid Cid { get; set; }


        public Diagnostic() { }

        public Diagnostic(DiagnosticType type, Cid cid) 
        {
            SetType(type);
            SetCid(cid);
        }

        private void SetCid(Entities.Cid cid)
        {
            Assertion.NotNull(cid, "Cid não informado.").Validate();

            this.Cid = cid;

            Assertion.Equals(this.Cid, cid, "Cid não foi atribuido corretamente.").Validate();
        }

        private void SetType(DiagnosticType type)
        {
            Assertion.NotNull(type, "Tipo não informado.").Validate();

            this.Type = type;

            Assertion.Equals(this.Type, type, "Tipo do diagnostico não foi informado corretamente.").Validate();
        }
    }
}
