using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR.Domain.Entities
{
    public class Hemotransfusion
    {
        public virtual bool Realised { get; set; }
        public virtual HemotransfusionTypeEnum Type { get; set; }
        public virtual bool TransfusionReaction { get; set; }
        public virtual IList<ReactionTypeEnum> Reactions { get; set; }
    }
}
