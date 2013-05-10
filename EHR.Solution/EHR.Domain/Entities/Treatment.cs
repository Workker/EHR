using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.CoreShared;
using EHR.Domain.Entities.TreatmentSumary;

namespace EHR.Domain.Entities
{
    public class Treatment : ITreatmentDTO
    {
        public string Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DbEnum Hospital { get; set; }
    }
}
