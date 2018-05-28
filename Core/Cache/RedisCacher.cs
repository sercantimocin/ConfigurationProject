using System;
using StackExchange.Redis.Extensions.Core;

namespace Core.Cache
{
    public class RedisCacher : ICacher
    {
        private readonly ICacheClient _redisClient;

        public RedisCacher(ICacheClient redisClient)
        {
            _redisClient = redisClient;
        }

        public T Get<T>(string key)
        {
            if (Exists(key))
            {
                T redisValue = _redisClient.Get<T>(key);
                return redisValue;
            }

            return default(T);
        }

        public void Set<T>(string key, T value, DateTime expireDate)
        {
            _redisClient.Add(key, value, expireDate);
        }

        private bool Exists(string key)
        {
            return _redisClient.Exists(key);
        }

        public void Dispose()
        {
            _redisClient?.Dispose();
        }
    }
}