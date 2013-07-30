using EHR.Domain.Entities.Interfaces;
using EHR.Domain.Util;
using System;
using System.Collections.Generic;

namespace EHR.Domain.Entities
{
    public class Account : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string Crm { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual GenderEnum Gender { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime? Birthday { get; set; }
        private IList<Hospital> _hospitals;
        public virtual IList<Hospital> Hospitals
        {
            get { return _hospitals ?? (_hospitals = new List<Hospital>()); }
            set { _hospitals = value; }
        }
        public virtual bool Approved { get; set; }
        public virtual bool Refused { get; set; }
        public virtual bool Administrator { get; set; }

        public virtual void EncryptPassword()
        {
            Password = CryptographyUtil.EncryptToSha512(Password);
        }
    }
}