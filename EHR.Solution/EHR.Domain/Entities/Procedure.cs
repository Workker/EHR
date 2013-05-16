using System;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Procedure
    {
        public virtual int Id { get; set; }
        public virtual Tus Tus { get; set; }
        public virtual DateTime Date { get; set; }

        public Procedure()
        {
        }

        public Procedure(Tus tus, DateTime date)
        {
            this.Tus = tus;
            this.Date = date;
        }

        public virtual void SetDate(DateTime date)
        {
            Assertion.NotNull(date, "Data inválida.").Validate();
            Assertion.IsTrue(date > DateTime.MinValue, "Data inválida.").Validate();

            this.Date = date;

            Assertion.IsTrue(this.Date == date, "Data inválida.").Validate();
        }

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