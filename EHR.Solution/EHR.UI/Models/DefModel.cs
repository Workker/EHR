using System;

namespace EHR.UI.Models
{
    [Serializable]
    public class DefModel
    {
        public short Id { get; set; }
        public string Description { get; set; }
        public short Code { get; set; }
    }
}