using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum OrientationOfMultidisciplinaryTeamsMetEnum : short
    {
        [Description("Sim")]
        Yes = 1,
        [Description(" Não se aplica")]
        NotApplicable = 2
    }
}