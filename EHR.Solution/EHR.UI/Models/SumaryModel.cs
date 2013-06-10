using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHR.UI.Models
{
    public class SumaryModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public AccountModel Doctor { get; set; }
        public PatientModel Patient { get; set; }
    }
}