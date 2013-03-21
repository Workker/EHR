using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using EHR.Domain.Repository;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace EHR.Test.Domain.Mapping
{
    [TestFixture]
    public class DiagnosticMapTest
    {
        [Test]
        [Ignore]
        public void test_mapping_of_diagnostic()
        {
            new PersistenceSpecification<Diagnostic>(session: BaseRepository.CreateSessionFactory().OpenSession()).
                CheckProperty(x => x.Id, 1).CheckProperty(x => x.CidCode, "test").CheckProperty(x => x.Cid, "test").CheckProperty(x => x.Type, "test").
                VerifyTheMappings();
        }
    }
}
