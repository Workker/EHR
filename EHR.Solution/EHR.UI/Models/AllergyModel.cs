using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class AllergyModel
    {
        public int Id { get; set; }
        public bool HaveAllergy { get; set; }
        public string TheWitch { get; set; }
        public List<string> Types { get; set; }
    }
}