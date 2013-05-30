using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum GenderEnum : short
    {
        [Description("Feminino")]
        Female = 1,
        [Description("Masculino")]
        Male = 2
    }
}