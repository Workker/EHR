using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum AllergyTypeEnum : short
    {
        [Description("Angioedema")]
        Angioedema = 1,
        [Description("Urticária")]
        Urticaria = 2,
        [Description("Choque Anafilático")]
        ChoqueAnafilatico = 3,
        [Description("Broncoespasmo")]
        Broncoespasmo = 4,
        [Description("Laringoespasmo")]
        Laringoespasmo = 5,
        [Description("Outros")]
        Outros = 6
    }
}