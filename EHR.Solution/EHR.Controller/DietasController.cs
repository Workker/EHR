using EHR.CoreShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workker.Framework.Domain;

namespace EHR.Controller
{
   public class DietasController : EhrController
    {

        [ExceptionLogger]
        public override List<ValueObject> GetPrescription(string term)
        {
            List<ValueObject> dietas = new List<ValueObject>();
            dietas.Add(new Dieta() { Id = 1, Description = "Dieta Gracie" });
            dietas.Add(new Dieta() { Id = 2, Description = "Dieta Paleolítica" });
            dietas.Add(new Dieta() { Id = 3, Description = "Dieta Rápida de 7 Dias" });
            dietas.Add(new Dieta() { Id = 4, Description = "Dieta Detox 7 Dias" });
            dietas.Add(new Dieta() { Id = 5, Description = "Dieta Detox Super Rápida" });
            dietas.Add(new Dieta() { Id = 6, Description = "Dieta do Leite" });
            dietas.Add(new Dieta() { Id = 7, Description = "Dieta Seca Barriga" });
            dietas.Add(new Dieta() { Id = 8, Description = "Dieta dos Alimentos Crus" });
            dietas.Add(new Dieta() { Id = 9, Description = "Dieta Macrobiótica" });

            Assertion.NotNull(dietas, "Lista de dietas nula.").Validate();

            return dietas;
        }
    }
}
