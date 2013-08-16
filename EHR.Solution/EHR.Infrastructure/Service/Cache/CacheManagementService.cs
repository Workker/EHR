using EHRCache;

namespace EHR.Infrastructure.Service.Cache
{
    public static class CacheManagementService
    {
        private static readonly RedisClient CacheClient = new RedisClient();

        public static void SetIn<T>(int database, string key, T obj)
        {
            CacheClient.Set(database, key, obj);
        }

        public static T GetBy<T>(string key)
        {
            return CacheClient.GetBy<T>(key);
        }
    }
}
