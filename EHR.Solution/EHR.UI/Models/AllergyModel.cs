using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class AllergyModel
    {
        public int Id { get; set; }
        public string TheWhich { get; set; }
        private IList<short> _Types { get; set; }
        public virtual IList<short> Types
        {
            get { return _Types ?? (_Types = new List<short>()); }
        }
    }
}