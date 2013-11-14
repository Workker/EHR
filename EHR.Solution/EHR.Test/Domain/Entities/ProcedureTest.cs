using EHR.CoreShared.Entities;
using EHR.Domain.Entities;
using NUnit.Framework;
using System;

namespace EHR.Test.Domain.Entities
{
    [TestFixture]
    public class ProcedureTest
    {
        [Test]
        public void set_date_witch_sucess()
        {
            var procedure = new Procedure();
            procedure.SetDate(DateTime.Now);

            Assert.IsTrue(procedure.Date != null);
        }

        [Test]
        public void get_code_witch_sucess()
        {
            var procedure = new Procedure { Tus = new TUSS { Code = "03" }, Date = DateTime.Now };
            Assert.IsTrue(procedure.GetCode() != string.Empty);
        }

        [Test]
        public void get_description_witch_sucess()
        {
            var procedure = new Procedure { Tus = new TUSS { Description = "desc" }, Date = DateTime.Now };
            Assert.IsTrue(procedure.GetDescription() != string.Empty);
        }

    }
}
