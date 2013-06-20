using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHR.UI.Models
{
    public class MedicationModel
    {
        public int Id { get; set; }
        public short MedicationType { get; set; }
        public short Def { get; set; }
        public string Presentation { get; set; }
        public string PresentationType { get; set; }
        public string Dose { get; set; }
        public string Dosagem { get; set; }
        public string Via { get; set; }
        public string Local { get; set; }
        public string Frequency { get; set; }
        public string FrequencyCase { get; set; }
        public string Duration { get; set; }
    }
}