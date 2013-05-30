using EHR.Domain.Entities.Interfaces;
using EHR.Domain.Util;
using System;
using System.Collections.Generic;

namespace EHR.Domain.Entities
{
    public class Account : IAggregateRoot<int>
    {
        public virtual int Id { get; set; }
        public virtual string CRM { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual GenderEnum Gender { get; set; }
        public virtual string Email { get; set; }
        private string _password;
        public virtual string Password
        {
            get { return _password; }
            set
            {
                _password = CryptographyUtil.ConvertStringToMd5(value);
            }
        }
        public virtual DateTime? Birthday { get; set; }
        private IList<Hospital> _hospitals;
        public virtual IList<Hospital> Hospitals
        {
            get { return _hospitals ?? (_hospitals = new List<Hospital>()); }
            set { _hospitals = value; }
        }
        public virtual bool Approved { get; set; }
    }
}