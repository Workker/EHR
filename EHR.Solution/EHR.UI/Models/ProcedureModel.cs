using System;

namespace EHR.UI.Models
{
    public class ProcedureModel
    {
        public TUSSModel TUSS { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public int Id { get; set; }
    }
}