using System;

namespace EHR.Domain.Entities
{
    [Serializable]
    public class Tus : ValueObject
    {
        public virtual string Code { get; set; }
    }
}

