using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum ConditionOfThePatientAtHighEnum : short
    {
        [Description("Curado")]
        Cured = 1,
        [Description("Melhorado")]
        Improved = 2,
        [Description("Desistência de tratamento")]
        WithdrawalOfTreatment = 3
    }
}
