using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class HemotransfusionModel
    {
        public int Id { get; set; }
        public short HemotransfusionType { get; set; }
        private List<short> _ReactionTypes { get; set; }
        public IList<short> ReactionTypes
        {
            get { return _ReactionTypes ?? (_ReactionTypes = new List<short>()); }
        }
    }
}