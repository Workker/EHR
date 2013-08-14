using System;
using EHR.CoreShared;
using Workker.Framework.Domain;

namespace EHR.Domain.Entities
{
    public class Procedure
    {
        public virtual int Id { get; set; }
        public virtual TUS Tus { get; set; }
        public virtual DateTime? Date { get; set; }

        public virtual void SetDate(DateTime date)
        {
            Assertion.NotNull(date, "Data inválida.").Validate();
            Assertion.IsTrue(date > DateTime.MinValue, "Data inválida.").Validate();

            Date = date;

            Assertion.IsTrue(Date == date, "Data inválida.").Validate();
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