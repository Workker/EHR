using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum ReasonOfAdmissionEnum:short
    {
        [Description("Eletiva")]
        Elective = 1,
        [Description("Emergência")]
        Emergency = 2,
        [Description("Clínica")]
        Clinic = 3,
        [Description("Cirúrgica")]
        Cirurgic = 4
    }
}
