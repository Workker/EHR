using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum DosageEnum : short
    {
        [Description("ml")]
        Ml = 1,
        [Description("mg")]
        Mg = 2,
        [Description("mcg")]
        Mcg = 3,
        [Description("Gota(s)")]
        Drops = 4,
        [Description("comprimido(s)")]
        Tablets = 5
    }
}
