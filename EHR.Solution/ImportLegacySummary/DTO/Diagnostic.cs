using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportLegacySummary.DTO
{
    public class Diagnostic
    {
        public long DiagnosticId { get; set; }
        public string Cid { get; set; }
        public string Type { get; set; }
    }
}
