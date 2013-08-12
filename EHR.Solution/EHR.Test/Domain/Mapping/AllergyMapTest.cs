using EHR.Domain.Entities;
using EHR.Domain.Repository;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace EHR.Test.Domain.Mapping
{
    [TestFixture]
    public class AllergyMapTest : AbstractInMemoryDataFixture
    {
        [Test]
        [Ignore]
        public void test_mapping_of_allergy()
        {
            new PersistenceSpecification<Allergy>(Session).
                CheckProperty(x => x.Id, 1).CheckProperty(x => x.TheWhich, "Test").
                CheckProperty(x => x.Types, 1).CheckProperty(x => x.Types, "Angioedema").VerifyTheMappings();

        }
    }
}
