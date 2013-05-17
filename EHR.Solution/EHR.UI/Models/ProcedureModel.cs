using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHR.UI.Models
{
    public class ProcedureModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public int Id { get; set; }
    }
}