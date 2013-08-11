using EHR.Domain.Repository;
using NUnit.Framework;

namespace EHR.Test.Domain.Repository
{
    [TestFixture]
    public class SummariesTest
    {
        [Test]
        [Ignore]
        public void Get_all_summaries_of_one_cpf()
        {
            var summaries = new Summaries();
            var result = summaries.GetAllSummaries("37179780715");
            Assert.AreEqual(1, result.Count);
        }
    }
}
