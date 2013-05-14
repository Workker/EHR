using EHR.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var procedure = new Procedure(new Tus() { Code = "03" }, DateTime.Now);
            Assert.IsTrue(procedure.GetCode() != string.Empty);
        }

        [Test]
        public void get_description_witch_sucess()
        {
            var procedure = new Procedure(new Tus() { Description = "desc" }, DateTime.Now);
            Assert.IsTrue(procedure.GetDescription() != string.Empty);
        }

    }
}
