using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportLegacySummary.DTO
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string Presentation { get; set; }
        public string PresentationType { get; set; }
        public string Dose { get; set; }
        public string Dosage { get; set; }
        public string Way { get; set; }
        public string Place { get; set; }
        public string Frequency { get; set; }
        public string FrequencyCase { get; set; }
        public int Duration { get; set; }
    }
}
