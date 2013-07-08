using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum WayEnum : short
    {
        [Description("Oral")]
        Oral = 1,
        [Description("Enteral")]
        Enteral = 2,
        [Description("Retal")]
        Rectal = 3,
        [Description("Sublingual")]
        Sublingually = 4,
        [Description("Subcutânea")]
        Subcutaneously = 5,
        [Description("Intramuscular")]
        Intramuscular = 6,
        [Description("Intravenosa")]
        Intravenously = 7,
        [Description("Transdérmico")]
        Transdermal = 8,
        [Description("Intracavitária")]
        Cavity = 9,
        [Description("Intravaginal")]
        Intravaginal = 11,
        [Description("Nasal")]
        Nasal = 12,
        [Description("Auricular")]
        Headset = 13,
        [Description("Ocular")]
        Eye = 14,
        [Description("Tópico")]
        Topic = 15
    }
}
