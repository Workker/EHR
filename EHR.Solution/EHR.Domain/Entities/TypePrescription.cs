using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Entities
{
    public enum TypePrescription : short
    {
        Medicamentos = 1,
        Cuidados,
        Dietas,
        Procedimentos,
        SuporteVentilatório,
        Monitorização,
        Hemocomponentes
    }
}
