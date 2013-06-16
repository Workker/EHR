using System;

namespace EHR.UI.Models
{
    public class TreatmentModel
    {
        public string Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public short Hospital { get; set; }
    }
}