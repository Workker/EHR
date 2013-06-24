using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum ExamTypeEnum : short
    {
        [Description("Bioquímica")]
        Bioquimica = 1,
        [Description("Especializados")]
        Especializados = 2,
        [Description("Hematologia")]
        Hematologia = 3,
        [Description("Hormônio")]
        Hormonio = 4,
        [Description("Micro Biologia")]
        MicroBiologia = 5,
        [Description("Patologia")]
        Patologia = 6
    }
}
