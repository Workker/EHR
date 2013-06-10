using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHR.UI.Models
{
    public class PatientModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateBirthday { get; set; }
        public string CPF { get; set; }
        public string Hospital { get; set; }
    }
}