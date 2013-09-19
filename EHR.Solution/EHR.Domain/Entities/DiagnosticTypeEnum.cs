using System.ComponentModel;

namespace EHR.Domain.Entities
{
    public enum DiagnosticTypeEnum : short
    {
        [Description("Principal")]
        Principal = 1,
        [Description("Associados e/ou Outros")]
        AssociadosEOuOutros = 2
    }
}
