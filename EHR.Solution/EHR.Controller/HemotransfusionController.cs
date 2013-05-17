using System.Collections.Generic;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using Workker.Framework.Domain;

namespace EHR.Controller
{
    public class HemotransfusionController : EHRController
    {
        private Types<HemotransfusionType> hemotransfusionTypes;
        public Types<HemotransfusionType> HemotransfusionTypes
        {
            get { return hemotransfusionTypes ?? (hemotransfusionTypes = new Types<HemotransfusionType>()); }
            set
            {
                hemotransfusionTypes = value;
            }
        }

        private Types<ReactionType> reactionTypes;
        public Types<ReactionType> ReactionTypes
        {
            get { return reactionTypes ?? (reactionTypes = new Types<ReactionType>()); }
            set
            {
                reactionTypes = value;
            }
        }
        

        public override void SaveHemotransfusion(List<string> typeReaction, string typeHemotrasfusion, Summary summary)
        {
            Assertion.NotNull(summary, "Não existe nenhum sumário selecionado para inserir o procedimento.").Validate();
            Assertion.NotNull(summary, "Sumário não informado.").Validate();

            List<ReactionType> reactions = GetReactions(typeReaction);
            var hemoType = HemotransfusionTypes.Get(short.Parse( typeHemotrasfusion));

            summary.CreateHemotransfusion(hemoType, reactions);

            Summaries.Save(summary);
        }

        private List<ReactionType> GetReactions(List<string> typeReaction)
        {
            List<ReactionType> reactions = new List<ReactionType>();

            if (typeReaction != null && typeReaction.Count > 0)
            {
                foreach (var type in typeReaction)
                {
                    FillReaction(reactions, type);
                }
            }

            return reactions;
        }

        private void FillReaction(List<ReactionType> reactions, string type)
        {
            Assertion.NotNull(reactions, "Lista de reações não foi inicializada.").Validate();
            Assertion.GreaterThan(short.Parse(type), short.Parse("0"), "tipo reação não informado.").Validate();

            var reaction = ReactionTypes.Get(short.Parse(type));
            reactions.Add(reaction);
        }

        public override void RemoveHemotransfusion(Summary summary, int id)
        {
            Assertion.NotNull(summary, "Sumário não informado.").Validate();
            Assertion.GreaterThan(id, 0, "Hemotransfusão não informada.").Validate();

            summary.RemoveHemotransfusion(id);
            Summaries.Save(summary);
        }

    }
}
