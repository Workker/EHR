using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.UI.Models
{

    [Serializable]
    public class ValueObjectModel
    {
        public short Id { get; set; }
        public string Description { get; set; }
        public short Code { get; set; }
    }
}
