
namespace EHR.UI.Models
{
    public class ProfessionalRegistrationModel
    {
        public virtual int Id { get; set; }
        public virtual string Number { get; set; }
        public virtual short Type { get; set; }
        public virtual short State { get; set; }
    }
}