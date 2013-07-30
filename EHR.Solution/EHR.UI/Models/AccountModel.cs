using System;
using System.Collections.Generic;

namespace EHR.UI.Models
{
    [Serializable]
    public class AccountModel
    {
        public virtual int Id { get; set; }
        public string Crm { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public bool Administrator { get; set; }
        public short CurrentHospital { get; set; }
        private IList<short> _hospitals;
        public IList<short> Hospitals
        {
            get { return _hospitals ?? (_hospitals = new List<short>()); }
            set { _hospitals = value; }
        }
    }
}