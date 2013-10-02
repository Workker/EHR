using EHR.CoreShared;

namespace EHR.Domain.Entities
{
    public class ProfessionalRegistration
    {
        public virtual int Id { get; set; }
        public virtual string Number { get; set; }
        public virtual ProfessionalRegistrationTypeEnum Type { get; set; }
        public virtual State State { get; set; }
        public virtual bool Approved { get; set; }
        public virtual bool Refused { get; set; }

        public virtual void ToApprove(bool approved)
        {
            Approved = approved;
        }

        public virtual void ToRefuse(bool refused)
        {
            Refused = refused;
        }
    }
}