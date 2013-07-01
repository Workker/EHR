using EHR.Domain.Entities.Interfaces;
using System;

namespace EHR.Domain.Entities
{
    public class View : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual DateTime VisiteDate { get; set; }
        public virtual Account Account { get; set; }
    }
}
