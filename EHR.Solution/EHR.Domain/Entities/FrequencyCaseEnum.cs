using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum FrequencyCaseEnum : short
    {
        [Description("Dor")]
        Pain = 1,
        [Description("Febre")]
        Fever = 2,
        [Description("Náusea")]
        Nausea = 3,
        [Description("Diarréia")]
        Diarrhea = 4,
        [Description("Constipação ")]
        Constipation = 5
    }
}
