using System;

namespace EHR.UI.Models
{
    public class ViewModel
    {
        public int Id { get; set; }
        public DateTime VisiteDate { get; set; }
        public AccountModel Account { get; set; }

        public override int GetHashCode()
        {
            return Account.Id;
        }

        
    }
}