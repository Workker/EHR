
using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum ProfessionalRegistrationTypeEnum : short
    {
        [Description("CRM")]
        CRM = 1,
        [Description("CRO")]
        CRO = 2
    }
}