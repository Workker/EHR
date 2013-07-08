using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum PresentationTypeEnum : short
    {
        [Description("Creme")]
        Cream = 2,
        [Description("Pomada")]
        Ointment = 3,
        [Description("Aerosol")]
        Aerosol = 4,
        [Description("Drágea")]
        Tableta = 5,
        [Description("Solução Oral")]
        OralSolution = 6,
        [Description("Solução Injetável")]
        InjectableSolution = 7,
        [Description("Xarope")]
        Syrup = 8,
        [Description("Gota")]
        Drop = 9,
        [Description("Supositório")]
        Suppository = 10,
        [Description("Óvulo")]
        Egg = 11,
        [Description("Comprimido")]
        Compressed = 12,
        [Description("Solução Nasal")]
        NasalSolution = 13,
        [Description("Solução Oftálmica")]
        OphthalmicSolution = 14,
        [Description("Cápsula")]
        Capsule = 15,
        [Description("Suspensão Oral")]
        OralSuspension = 16
    }
}
