using EHR.CoreShared;
using EHR.Domain.Entities;
using NUnit.Framework;
using System;

namespace EHR.Test.Domain.Entities
{
    [TestFixture]
    public class SummaryTest
    {
        #region Remove procedure
        [Test]
        public void remove_procedure_witch_sucess()
        {
            var summary = new Summary();

            summary.Procedures.Add(new Procedure { Id = 1 });
            summary.RemoveProcedure(1);

            Assert.IsTrue(summary.Procedures.Count == 0);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage = "Id não informado.")]
        public void remove_procedure_witch_id_equals_zero_must_return_exeption()
        {
            var summary = new Summary();
            summary.RemoveProcedure(0);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage = "Procedimento não encontrado.")]
        public void remove_procedure_witch_id_invalid_must_return_exeption()
        {
            var summary = new Summary();
            summary.RemoveProcedure(1);
        }
        #endregion

        #region Create Procedure

        [Test]
        public void create_procedure_witch_sucess()
        {
            var summary = new Summary();

            summary.CreateProcedure(1, 5, 2012, new TUSS { Id = 1 });

            Assert.IsTrue(summary.Procedures.Count > 0);
        }


        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage = "Mês inválido.")]
        public void create_procedure_witch_month_equals_zero_must_return_exeption()
        {
            var summary = new Summary();
            summary.CreateProcedure(0, 5, 2012, new TUSS { Id = 1 });
        }

        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage = "Dia inválido.")]
        public void create_procedure_witch_day_equals_zero_must_return_exeption()
        {
            var summary = new Summary();
            summary.CreateProcedure(1, 0, 2011, new TUSS { Id = 1 });
        }

        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage = "Ano inválido.")]
        public void create_procedure_witch_year_equals_zero_must_return_exeption()
        {
            var summary = new Summary();
            summary.CreateProcedure(1, 5, 0, new TUSS { Id = 1 });
        }

        //[Test]
        //[ExpectedException(typeof(ApplicationException), ExpectedMessage = "Tus não informado.")]
        //public void create_procedure_witch_tus_null_must_return_exeption()
        //{
        //    var summary = new Summary();
        //    summary.CreateProcedure(1, 5, 2011, null);
        //}

        [Test]
        [ExpectedException(typeof(ApplicationException), ExpectedMessage = "Tus inválido.")]
        public void create_procedure_witch_tus_witch_id_equals_zero_must_return_exeption()
        {
            var summary = new Summary();
            summary.CreateProcedure(1, 5, 2011, new TUSS { Id = 0 });
        }

        #endregion
    }
}
