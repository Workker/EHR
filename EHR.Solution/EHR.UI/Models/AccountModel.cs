using System;
using System.Collections.Generic;

namespace EHR.UI.Models
{
    public class AccountModel
    {
        public virtual int Id { get; set; }
        public string CRM { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        private IList<short> _hospitals;
        public virtual IList<short> Hospitals
        {
            get { return _hospitals ?? (_hospitals = new List<short>()); }
            set { _hospitals = value; }
        }
    }
}