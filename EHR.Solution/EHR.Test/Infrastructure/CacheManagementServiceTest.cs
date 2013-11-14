using EHR.CoreShared.Entities;
using EHR.Domain.Repository;
using EHR.Infrastructure.Service.Cache;
using NUnit.Framework;
using System.Collections.Generic;

namespace EHR.Test.Infrastructure
{
    [TestFixture]
    public class CacheManagementServiceTest
    {
        [Test]
        public void set_hospitals_in_cache()
        {
            var repository = new Hospitals();
            var list = repository.GetAll();

            CacheManagementService.SetIn(1, "Hospitals", list);

            var cache = CacheManagementService.GetBy<IList<Hospital>>("Hospitals");

            Assert.AreEqual(list.Count, cache.Count);
        }
    }
}
