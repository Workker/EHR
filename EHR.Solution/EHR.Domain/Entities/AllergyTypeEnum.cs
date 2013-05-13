using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum AllergyTypeEnum : short
    {
        [Description("Angioedema")]
        Angioedema = 0,
        [Description("Urticária")]
        Urticaria = 1,
        [Description("Choque Anafilático")]
        ChoqueAnafilatico = 2,
        [Description("Broncoespasmo")]
        Broncoespasmo = 3,
        [Description("Laringoespasmo")]
        Laringoespasmo = 4,
        [Description("Outros")]
        Outros = 5
    }
}