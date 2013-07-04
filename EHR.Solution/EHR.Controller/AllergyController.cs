using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AllergyController : EHRController
    {
        private Types<AllergyType> _allergyTypes;
        public Types<AllergyType> AllergyTypes
        {
            get { return _allergyTypes ?? (_allergyTypes = new Types<AllergyType>()); }
            set
            {
                _allergyTypes = value;
            }
        }

        public override void SaveAllergy(string theWitch, IList<short> types, int idSummary)
        {
            Assertion.IsFalse(string.IsNullOrEmpty(theWitch), "Motivo da alergia não informado.").Validate();
            Assertion.GreaterThan(types.Count, 0, "Não foi selecionado um tipo de alergia.").Validate();
            Assertion.GreaterThan(idSummary, 0, "Summario de alta inválido.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            var allergies = new List<AllergyType>();

            foreach (var id in types)
            {
                var type = GetAllergy(id);
                allergies.Add(type);
            }

            summary.CreateAllergy(theWitch, allergies);

            Summaries.Save(summary);

            //todo: do
        }

        public override void RemoveAllergy(int summaryId, int alleryId)
        {
            Assertion.GreaterThan(summaryId, 0, "Sumario de alta inválido.").Validate();
            Assertion.GreaterThan(alleryId, 0, "Alergia inválida.").Validate();

            var summary = Summaries.Get<Summary>(summaryId);

            summary.RemoveAllergy(alleryId);
            Summaries.Save(summary);

            //todo: do
        }

        private AllergyType GetAllergy(short id)
        {
            Assertion.GreaterThan(id, short.Parse("0"), "Alergia deve ser informada.").Validate();

            var type = AllergyTypes.Get(id);

            Assertion.NotNull(type, "Alergia não encontrada.").Validate();

            return type;
        }

    }
}
