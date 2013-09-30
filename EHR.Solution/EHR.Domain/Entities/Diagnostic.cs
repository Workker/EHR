using EHR.CoreShared;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Diagnostic
    {
        public virtual int Id { get; set; }
        public virtual DiagnosticType Type { get; set; }
        public virtual CID Cid { get; set; }
        public virtual string Description { get; set; }


        private void SetCid(CID cid)
        {
            Assertion.NotNull(cid, "Cid não informado.").Validate();

            Cid = cid;

            Assertion.Equals(Cid, cid, "Cid não foi atribuido corretamente.").Validate();
        }

        private void SetType(DiagnosticType type)
        {
            Assertion.NotNull(type, "Tipo não informado.").Validate();

            Type = type;

            Assertion.Equals(Type, type, "Tipo do diagnostico não foi informado corretamente.").Validate();
        }
    }
}
