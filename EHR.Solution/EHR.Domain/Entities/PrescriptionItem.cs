using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Entities
{
    public enum PrescriptionItemType : short
    {
        Medicamentos = 1,
        Cuidados,
        Dietas,
        Procedimentos,
        SuporteVentilatório,
        Monitorização,
        Hemocomponentes
    }

    public class PrescriptionItem : ValueObject
    {
        public virtual PrescriptionItemType PrescriptionItemType { get; set; }
        public virtual string code { get; set; }
        public virtual string ActivePrinciple { get; set; }

        //TODO: respeitar a regra: Ao Inserir novo item, obter prescriptionItem por prescriptionItemType + Code, retorno deve ser nulo.
    }
}
