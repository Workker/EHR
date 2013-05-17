using EHR.Domain.Entities;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class HemotransfusionController : EHRController
    {

        public override void SaveHemotransfusion(Summary summary)
        {
            //Assertion.GreaterThan(int.Parse(dob_month), 0, "Mês inválido").Validate();
            //Assertion.GreaterThan(int.Parse(dob_day), 0, "Dia inválido").Validate();
            //Assertion.GreaterThan(int.Parse(dob_year), 0, "Ano inválido").Validate();
            //Assertion.IsFalse(string.IsNullOrEmpty(procedureCode), "Codigo do procedimento inválido").Validate();
            Assertion.NotNull(summary, "Não existe nenhum sumário selecionado para inserir o procedimento.");

            //summary.CreateHemotransfusion();

            Summaries.Save(summary);
        }

        public override void RemoveHemotransfusion(Summary summary, int id)
        {
            summary.RemoveHemotransfusion(id);
            Summaries.Save(summary);
        }

    }
}
