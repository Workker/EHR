using System;

namespace EHR.UI.Models
{
    [Serializable]
    public class SpecialtyModel
    {
        public short Id { get; set; }
        public string Description { get; set; }
        public short Code { get; set; }
    }
}