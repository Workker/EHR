using System;
using System.Collections.Generic;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Account : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string CRM { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime? Birthday { get; set; }

        private IList<Hospital> _hospitals;
        public virtual IList<Hospital> Hospitals
        {
            get { return _hospitals ?? (_hospitals = new List<Hospital>()); }
        }
    }
}