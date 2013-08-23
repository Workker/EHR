using ProtoBuf;

namespace EHR.Domain.Entities
{
    [ProtoContract]
    [ProtoInclude(5, typeof(AllergyType))]
    [ProtoInclude(6, typeof(ConditionAtDischarge))]
    [ProtoInclude(7, typeof(DiagnosticType))]
    public abstract class ValueObject : CoreShared.ValueObject
    {
        [ProtoMember(1)]
        public virtual short Id { get; set; }
        [ProtoMember(2)]
        public virtual string Description { get; set; }
    }
}
