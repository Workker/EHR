using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Tus : IAggregateRoot<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Procedure { get; set; }
    }
}

