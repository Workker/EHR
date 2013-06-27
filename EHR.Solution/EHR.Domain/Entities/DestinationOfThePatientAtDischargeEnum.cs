using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum DestinationOfThePatientAtDischargeEnum : short
    {
        [Description("Ambulatorial")]
        OutPatient = 1,
        [Description("Domiciliar")]
        Home = 2,
        [Description("Outra Instituição")]
        AnotherInstitution = 3
    }
}