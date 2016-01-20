using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHR.UI.Models
{
    public class PrescriptionDateModel
    {
        public int PrescriptionHighMonth { get; set; }
        public int PrescriptionHighYear { get; set; }
        public int PrescriptionHighDay { get; set; }
        public int PrescriptionHighHour { get; set; }
        public int PrescriptionHighMinute { get; set; }
        public string PrescriptionDischargeTime { get; set; }
    }
}