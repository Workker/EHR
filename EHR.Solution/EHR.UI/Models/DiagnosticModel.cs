using EHR.Domain.Entities;

namespace EHR.UI.Models
{
    public class DiagnosticModel
    {
        public int Id { get; set; }
        public short Type { get; set; }
        public Cid Cid { get; set; }
    }
}