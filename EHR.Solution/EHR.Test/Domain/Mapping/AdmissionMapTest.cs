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
    public class AdmissionMapTest
    {
        [Test]
        public void test_mapping_of_admission()
        {
            new PersistenceSpecification<Admission>(session: BaseRepository.CreateSessionFactory().OpenSession())
                .CheckProperty(x => x.Id, 1).CheckProperty(x => x.ResonOfAdmission, 1).VerifyTheMappings();
        }
    }
}
