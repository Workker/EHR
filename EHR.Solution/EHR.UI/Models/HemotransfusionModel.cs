using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class HemotransfusionModel
    {
        public int Id { get; set; }
        public short HemotransfusionType { get; set; }
        public List<short> ReactionType { get; set; }
    }
}