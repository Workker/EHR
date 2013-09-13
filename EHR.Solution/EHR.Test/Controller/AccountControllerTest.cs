using EHR.Controller;
using EHR.Domain.Repository;
using NUnit.Framework;

namespace EHR.Test.Controller
{
    [TestFixture]
    public class AccountControllerTest
    {
        [Test]
        public void Add_professional_Resgistration()
        {
            var accounts = new Accounts();
            var account = accounts.GetBy(3);

            FactoryController.GetController(ControllerEnum.Account).AddprofessionalResgistration(account.Id, 1, "123456", 13);


            var accountB = accounts.GetBy(3);

            Assert.GreaterOrEqual(accountB.ProfessionalRegistration.Count, 2);
        }
    }
}
