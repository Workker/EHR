using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Interfaces;

namespace EHR.Domain.Entities
{
    public class Procedure
    {
        public virtual int Id { get; set; }
        public virtual Tus Tus { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual string GetCode()
        {
           return Tus.Code;
        }

        public virtual string GetDescription()
        {
            return Tus.Description;
        }
    }
}