using EHR.Domain.Entities;
using FluentNHibernate.Testing;
using NUnit.Framework;
using System.Collections.Generic;

namespace EHR.Test.Domain.Mapping
{
    [TestFixture]
    public class AdmissionMapTest : AbstractInMemoryDataFixture
    {
        [Test]
        [Ignore]
        public void test_mapping_of_admission()
        {
            new PersistenceSpecification<Admission>(Session)
                .CheckProperty(x => x.Id, 1).CheckProperty(x => x.ReasonOfAdmission,
                new List<ReasonOfAdmission> { new ReasonOfAdmission { Id = 1, Description = "Teste" } }).VerifyTheMappings();
        }
    }
}
