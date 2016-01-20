using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
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
        public override List<PrescriptionItem> GetPrescription(string term)
        {
            List<PrescriptionItem> dietas = new List<PrescriptionItem>();
            dietas.Add(new PrescriptionItem() { Id = 1, Description = "Dieta Gracie" });
            dietas.Add(new PrescriptionItem() { Id = 2, Description = "Dieta Paleolítica" });
            dietas.Add(new PrescriptionItem() { Id = 3, Description = "Dieta Rápida de 7 Dias" });
            dietas.Add(new PrescriptionItem() { Id = 4, Description = "Dieta Detox 7 Dias" });
            dietas.Add(new PrescriptionItem() { Id = 5, Description = "Dieta Detox Super Rápida" });
            dietas.Add(new PrescriptionItem() { Id = 6, Description = "Dieta do Leite" });
            dietas.Add(new PrescriptionItem() { Id = 7, Description = "Dieta Seca Barriga" });
            dietas.Add(new PrescriptionItem() { Id = 8, Description = "Dieta dos Alimentos Crus" });
            dietas.Add(new PrescriptionItem() { Id = 9, Description = "Dieta Macrobiótica" });

            Assertion.NotNull(dietas, "Lista de dietas nula.").Validate();

            return dietas;
        }
    }
}
