﻿using EHR.CoreShared;

namespace EHR.Domain.Entities
{
    public class ProfessionalRegistration
    {
        public virtual int Id { get; set; }
        public virtual string Number { get; set; }
        public virtual ProfessionalRegistrationTypeEnum Type { get; set; }
        public virtual State State { get; set; }
        public virtual bool Approved { get; protected set; }
        public virtual bool Refused { get; protected set; }
    }
}