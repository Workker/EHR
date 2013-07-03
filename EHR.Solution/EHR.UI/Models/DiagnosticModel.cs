namespace EHR.UI.Models
{
    public class DiagnosticModel
    {
        public int Id { get; set; }
        public short Type { get; set; }
        public CidModel Cid { get; set; }
    }
}