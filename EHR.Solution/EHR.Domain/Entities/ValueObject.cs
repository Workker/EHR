using ProtoBuf;

namespace EHR.Domain.Entities
{
    [ProtoContract]
    [ProtoInclude(5, typeof(AllergyType))]
    [ProtoInclude(6, typeof(ConditionAtDischarge))]
    [ProtoInclude(7, typeof(DiagnosticType))]
    public abstract class ValueObject : CoreShared.Entities.ValueObject
    {
        [ProtoMember(1)]
        public override short Id { get; set; }
        [ProtoMember(2)]
        public override string Description { get; set; }
    }
}
