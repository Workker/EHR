using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportLegacySummary.DTO
{
    public class Procedure
    {
        public int ProcedureId { get; set; }
        public DateTime? DateProc { get; set; }
        public string TusCode { get; set; }
    }
}
