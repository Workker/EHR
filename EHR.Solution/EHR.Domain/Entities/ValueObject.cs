﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    [Serializable]
    public abstract class ValueObject : IAggregateRoot<short>
    {
        public virtual short Id { get; set; }
        public virtual string Description { get; set; }
    }
}
