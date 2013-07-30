using NUnit.Framework;

namespace EHR.Test.Domain.Entities
{
    [TestFixture]
    public class PatientsTest
    {
        //private Patient patient;

        [TestFixtureSetUp]
        public void SetUp()
        {
            //patient = new Patient();
        }

        [Test]
        [Ignore]
        public void add_new_admission()
        {
            //var reasons = new List<Reason> { Reason.Cirurgic };
            //var admission = new Admission { Id = 1, ReasonOfAdmission = reasons };

            //patient.AddAdmission(admission);

            //Assert.AreEqual(admission, patient.Admissions.First());
        }

        [Test]
        [Ignore]
        public void add_new_allergy()
        {
            //var allergy = new Allergy { Id = 1, HaveAllergies = true, TheWhich = "eggs", Type = TypeOfAllergy.Urticaria };

            //patient.AddAllergy(allergy);

            //Assert.AreEqual(allergy, patient.Allergies.First());
        }

        //[Test]
        //public void add_new_diagnostic()
        //{
        //    var diagnostic = new Diagnostic { Id = 1, CidCode = "123", Cid = "test", Type = "test" };

        //    patient.AddDiagnostic(diagnostic);

        //    Assert.AreEqual(diagnostic, patient.Diagnostics.First());
        //}
    }
}
