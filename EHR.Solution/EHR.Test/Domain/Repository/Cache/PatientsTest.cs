using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using NUnit.Framework;
using EHR.Domain.Repository.Cache;

namespace EHR.Test.Domain.Repository.Cache
{
    [TestFixture]
    public class PatientsTest
    {
        [Test]
        public void a_save_object_in_cache()
        {
            var patients = new Patients();
            var patient = new Patient { Id = 1, MedicinesOfUsePrior = "test", Annotations = "test test test test" };

            patients.Save(patient.Id.ToString(), patient);

            var memcachedClient = MemcachedClientHelper();

            var patientReturned = memcachedClient.Get<Patient>(patient.Id.ToString());

            Assert.AreEqual(patient.Id, patientReturned.Id);
        }

        [Test]
        public void b_get_objet_from_cache()
        {
            var patients = new Patients();
            var patientExpected = new Patient { Id = 1, MedicinesOfUsePrior = "test", Annotations = "test test test test" };
            var patientReturned = patients.GetBy(patientExpected.Id.ToString());

            Assert.AreEqual(patientExpected.Id, patientReturned.Id);
        }

        [Test]
        public void c_delete_object_from_cache()
        {
            var repository = new Patients();

            repository.Delete(1.ToString());
            var memcachedClient = MemcachedClientHelper();

            Assert.IsNull(memcachedClient.Get<Patient>(1.ToString()));
        }

        [Test]
        public void d_get_list_of_objects_from_cache()
        {
            var memcachedClient = MemcachedClientHelper();
            var keys = new List<string>();

            for (var i = 0; i <= 10; i++)
            {
                keys.Add(i.ToString());
                var patient = new Patient { Id = i, MedicinesOfUsePrior = "test" + i, Annotations = "test test test test" + i };
                memcachedClient.Store(StoreMode.Set, patient.Id.ToString(), patient);
            }

            var repository = new Patients();

            var result = repository.GetBy(keys);

            Assert.AreEqual(11, result.Count);
        }

        [Test]
        public void e_clean_cache()
        {
            var repository = new Patients();
            repository.CleanCache();

            var memcachedClient = MemcachedClientHelper();

            var keys = new List<string>
                           {
                               0.ToString(),
                               1.ToString(),
                               2.ToString(),
                               3.ToString(),
                               4.ToString(),
                               5.ToString(),
                               6.ToString(),
                               7.ToString(),
                               8.ToString(),
                               9.ToString(),
                               10.ToString()
                           };

           var result = memcachedClient.Get(keys);

            Assert.AreEqual(0, result.Count);

        }


        private static MemcachedClient MemcachedClientHelper()
        {
            var configuration = new MemcachedClientConfiguration();
            configuration.AddServer("127.0.0.1:11211");
            configuration.SocketPool.ReceiveTimeout = new TimeSpan(0, 0, 10);
            configuration.SocketPool.ConnectionTimeout = new TimeSpan(0, 0, 10);
            configuration.SocketPool.DeadTimeout = new TimeSpan(0, 0, 20);
            configuration.Protocol = MemcachedProtocol.Text;

            var memcachedClient = new MemcachedClient(configuration);
            return memcachedClient;
        }
    }
}
