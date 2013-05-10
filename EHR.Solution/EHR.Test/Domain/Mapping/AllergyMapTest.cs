using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities;
using EHR.Domain.Repository;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace EHR.Test.Domain.Mapping
{
    [TestFixture]
    public class AllergyMapTest
    {
        [Test]
        [Ignore]
        public void test_mapping_of_allergy()
        {
            new PersistenceSpecification<Allergy>(session: BaseRepository.CreateSessionFactory().OpenSession()).
                CheckProperty(x => x.Id, 1).CheckProperty(x => x.TheWhich, "Test").
                CheckProperty(x => x.HaveAllergies, true).CheckProperty(x => x.Type, 1).VerifyTheMappings();

        }
    }
}
