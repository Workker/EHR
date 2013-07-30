using EHR.Domain.Entities;
using EHR.Domain.Repository;
using System.Collections.Generic;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class HemotransfusionController : EhrController
    {
        private Types<HemotransfusionType> _hemotransfusionTypes;
        public Types<HemotransfusionType> HemotransfusionTypes
        {
            get { return _hemotransfusionTypes ?? (_hemotransfusionTypes = new Types<HemotransfusionType>()); }
            set
            {
                _hemotransfusionTypes = value;
            }
        }

        private Types<ReactionType> _reactionTypes;
        public Types<ReactionType> ReactionTypes
        {
            get { return _reactionTypes ?? (_reactionTypes = new Types<ReactionType>()); }
            set
            {
                _reactionTypes = value;
            }
        }

        [ExceptionLogger]
        public override void SaveHemotransfusion(IList<short> typeReaction, short typeHemotrasfusion, int idSummary)
        {
            Assertion.NotNull(typeReaction, "Lista de reações nula.").Validate();
            Assertion.GreaterThan((int)typeHemotrasfusion, 0, "Não foi informado o tipo de hemotransfusão.").Validate();
            Assertion.GreaterThan(idSummary, 0, "Sumário de alta inválido.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);
            var reactions = GetReactions(typeReaction);
            var hemoType = HemotransfusionTypes.Get(typeHemotrasfusion);

            summary.CreateHemotransfusion(hemoType, reactions);
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        public override void RemoveHemotransfusion(int idSummary, int id)
        {
            Assertion.GreaterThan(id, 0, "Hemotransfusão não informada.").Validate();
            Assertion.GreaterThan(id, 0, "Sumário de alta inválido.").Validate();

            var summary = Summaries.Get<Summary>(idSummary);

            summary.RemoveHemotransfusion(id);
            Summaries.Save(summary);

            //todo: do
        }

        [ExceptionLogger]
        private List<ReactionType> GetReactions(IList<short> typeReaction)
        {
            Assertion.NotNull(typeReaction, "Lista de tipos de reação nula.").Validate();

            var reactions = new List<ReactionType>();

            if (typeReaction != null && typeReaction.Count > 0)
            {
                foreach (var type in typeReaction)
                {
                    FillReaction(reactions, type);
                }
            }

            return reactions;
        }

        [ExceptionLogger]
        private void FillReaction(IList<ReactionType> reactions, short type)
        {
            Assertion.NotNull(reactions, "Lista de reações não foi inicializada.").Validate();
            Assertion.GreaterThan((int)type, 0, "Tipo reação não informado.").Validate();

            var reaction = ReactionTypes.Get(type);

            Assertion.NotNull(reaction, "Reação não encontrada.").Validate();

            reactions.Add(reaction);
        }
    }
}