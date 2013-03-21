using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHR.Domain.Entities.Patient;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace EHR.Domain.Repository.Cache
{
    public class Patients
    {
        private MemcachedClientConfiguration _configuration = new MemcachedClientConfiguration();
        private MemcachedClient _memcachedClient;

        public Patients()
        {
            SetMemcachedClientConfigurationSettings();
            _memcachedClient = new MemcachedClient(_configuration);
        }


        public Patient GetBy(string key)
        {
            return _memcachedClient.Get<Patient>(key);
        }

        public IList GetBy(IEnumerable<string> keys)
        {
            return _memcachedClient.ExecuteGet(keys).ToList();
        }

        public void Save(string key, Patient patient)
        {
           var test = _memcachedClient.Store(StoreMode.Set, key, patient);
        }

        public void Delete(string key)
        {
            _memcachedClient.Remove(key);
        }


        public void CleanCache()
        {
            _memcachedClient.FlushAll();
        }


        private void SetMemcachedClientConfigurationSettings()
        {
            _configuration.AddServer("127.0.0.1:11211");
            _configuration.SocketPool.ReceiveTimeout = new TimeSpan(0, 0, 10);
            _configuration.SocketPool.ConnectionTimeout = new TimeSpan(0, 0, 10);
            _configuration.SocketPool.DeadTimeout = new TimeSpan(0, 0, 20);
            _configuration.Protocol = MemcachedProtocol.Text;
        }
    }
}
