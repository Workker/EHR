using EHR.Domain.Entities;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace EHR.Test.Domain.Mapping
{
    [TestFixture]
    public class DiagnosticMapTest : AbstractInMemoryDataFixture
    {
        [Test]
        public void test_mapping_of_diagnostic()
        {
            new PersistenceSpecification<Diagnostic>(session: Session).
                CheckProperty(x => x.Id, 1).CheckProperty(x => x.CidCode, "test").CheckProperty(x => x.Cid, "test").CheckProperty(x => x.Type, "test").
                VerifyTheMappings();
        }
    }
}
