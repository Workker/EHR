using System;
using System.Collections.Generic;

namespace EHR.UI.Models
{
    [Serializable]
    public class AccountModel
    {
        public virtual int Id { get; set; }
        private IList<ProfessionalRegistrationModel> _professionalRegistration;
        public virtual IList<ProfessionalRegistrationModel> ProfessionalRegistration
        {
            get { return _professionalRegistration ?? (_professionalRegistration = new List<ProfessionalRegistrationModel>()); }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public bool Administrator { get; set; }
        public short CurrentHospital { get; set; }
        public short Hospital { get; set; }
    }
}