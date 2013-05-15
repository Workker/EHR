using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Entities
{
    public enum ReasonOfAdmissionEnum:short
    {
        [Description("Eletiva")]
        Elective = 1,
        [Description("Emergência")]
        Emergency = 2,
        [Description("Clínica")]
        Clinic = 3,
        [Description("Cirúrgica")]
        Cirurgic = 4
    }
}
