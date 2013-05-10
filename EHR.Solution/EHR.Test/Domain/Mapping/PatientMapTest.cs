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
    public class PatientMapTest
    {
        //[Test]
        //[Ignore]
        //public void test_mapping_of_patient()
        //{
        //    new PersistenceSpecification<Patient>(session: BaseRepository.CreateSessionFactory().OpenSession()).
        //        CheckProperty(x => x.Id, 1).CheckProperty(x => x.MedicinesOfUsePrior, "test").CheckProperty(
        //            x => x.Annotations, "test").VerifyTheMappings();
        //}
    }
}
