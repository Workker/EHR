using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum HemotransfusionTypeEnum : short
    {
        [Description("Criopreciptado")]
        Criopreciptado = 1,
        [Description("Concentrado de hemácias")]
        ConcentradoDeHemacias = 2,
        [Description("Concentrado de neutrófilos")]
        ConcentradoDeNeutrofilos = 3,
        [Description("Concentrado de plaquetas")]
        ConcentradoDePlaquetas = 4,
        [Description("Plasma")]
        Plasma = 5
    }
}
