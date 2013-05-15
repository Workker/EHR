using EHR.Domain.Entities;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AllergyController : EHRController
    {
        public void SaveAllergy(string theWitch, IList<AllergyType> types, Summary summary)
        {
            Assertion.GreaterThan(types.Count, 0, "Não foi selecionado um tipo de alergia.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(theWitch), "Motivo da alergia não informado.").Validate();
            Assertion.NotNull(summary, "Não existe nenhum sumário selecionado para inserir o procedimento.");

            summary.CreateAllergy(theWitch, types);

            Summaries.Save(summary);
        }

        public void RemoveAllergy(Summary summary, int id)
        {
            summary.RemoveAllergy(id);
            Summaries.Save(summary);
        }
    }
}
