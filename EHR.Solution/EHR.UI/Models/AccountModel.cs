using System;

namespace EHR.UI.Models
{
    public class AccountModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public short Gender { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
    }
}