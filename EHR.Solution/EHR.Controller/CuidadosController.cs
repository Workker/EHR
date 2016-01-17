using EHR.CoreShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workker.Framework.Domain;

namespace EHR.Controller
{
  

    public class CuidadosController : EhrController
    {
       
        [ExceptionLogger]
        public override List<CuidadoMedico> GetCuidadosMedicos(string term)
        {
            List<CuidadoMedico> cuidadoMedico = new List<CuidadoMedico>();
            cuidadoMedico.Add(new CuidadoMedico() {Id = 1,Description = "Passar Sonda Neo-Enteral" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Passar Sonda Neo-Enteral" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Passar Sonda Vesical de alivio" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Passar SVD" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Realizar Balanço Hidrico" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Realizar Banho de Aspersão" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Realizar Banho no Leito" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Trocar Curativos" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Verificar Temperatura" });

            Assertion.NotNull(cuidadoMedico, "Lista de medicamentos nula.").Validate();

            return cuidadoMedico;
        }

        [ExceptionLogger]
        public override List<ValueObject> GetPrescription(string term)
        {
            List<ValueObject> cuidadoMedico = new List<ValueObject>();
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Passar Sonda Neo-Enteral" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Passar Sonda Neo-Enteral" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Passar Sonda Vesical de alivio" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Passar SVD" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Realizar Balanço Hidrico" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Realizar Banho de Aspersão" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Realizar Banho no Leito" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Trocar Curativos" });
            cuidadoMedico.Add(new CuidadoMedico() { Id = 1, Description = "Verificar Temperatura" });

            Assertion.NotNull(cuidadoMedico, "Lista de medicamentos nula.").Validate();

            return cuidadoMedico;
        }


    }
}
