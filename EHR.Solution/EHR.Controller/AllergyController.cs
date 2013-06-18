using EHR.Domain.Entities;
using System.Collections.Generic;
using EHR.Domain.Repository;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class AllergyController : EHRController
    {
        #region Properties

        private Types<AllergyType> allergyTypes;
        public Types<AllergyType> AllergyTypes
        {
            get { return allergyTypes ?? (allergyTypes = new Types<AllergyType>()); }
            set
            {
                allergyTypes = value;
            }
        }

        #endregion

        public override void SaveAllergy(string theWitch, IList<short> types, int idSummary)
        {
            Assertion.GreaterThan(types.Count, 0, "Não foi selecionado um tipo de alergia.").Validate();
            Assertion.IsFalse(string.IsNullOrEmpty(theWitch), "Motivo da alergia não informado.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            var allergies = new List<AllergyType>();

            foreach (var id in types)
            {
                var type = GetAllergy(id);
                allergies.Add(type);
            }

            summary.CreateAllergy(theWitch, allergies);

            Summaries.Save(summary);
        }

        public override void RemoveAllergy(int idSummary, int id)
        {
            var summary = Summaries.Get<Summary>(idSummary);
            summary.RemoveAllergy(id);
            Summaries.Save(summary);
        }

        #region Private Methods

        private AllergyType GetAllergy(short id)
        {
            Assertion.GreaterThan(id, short.Parse("0"), "Alergia deve ser informada.").Validate();
            AllergyType type = AllergyTypes.Get((short)id);
            Assertion.NotNull(type, "Alergia não encontrada.").Validate();
            return type;
        }

        #endregion
    }
}
