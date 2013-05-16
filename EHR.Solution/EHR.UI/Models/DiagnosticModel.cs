using EHR.Domain.Entities;

namespace EHR.UI.Models
{
    public class DiagnosticModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Cid Cid { get; set; }
    }
}