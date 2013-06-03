

namespace EHR.Domain.Entities
{
    public class Hospital : ValueObject
    {
        public virtual string Name { get; set; }
        public virtual string URLImage { get; set; }
    }
}
