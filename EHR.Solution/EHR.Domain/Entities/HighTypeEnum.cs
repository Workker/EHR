using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum HighTypeEnum : short
    {
        [Description("Alta da Internação")]
        DischargedFromInpatient = 1,
        [Description(" Alta da Emergência")]
        HighOfEmergency = 2
    }
}
