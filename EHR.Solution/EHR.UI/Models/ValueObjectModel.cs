using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.UI.Models
{

    [Serializable]
    public class ItemPrescriptionModel
    {
        public short Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public short Type { get; set; }
    }
}
