using System;
using ConsoleApp2;
using Core.Cache;
using Data;
using Data.Repository;
using Model;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Core
{
    public class ConfigurationReader : ConfigurationBase, IConfigurationReader
    {
        private readonly string _applicationName;
        private readonly int _refreshTimerIntervalInMs;

        //<remark> Tight Coupled kod. Dışarıya interfaceler ile açılacak şekilde hazırladım.Constructer ın parametreleri elimde olmadığı için Loose Couple değil.</remark>
        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
            : base(new RedisCacher(new StackExchangeRedisCacheClient(RedisConnectionFactory.GetConnection(), new NewtonsoftSerializer())),
                new ConfigurationRepository(new DataHelper()), 
                connectionString)
        {
            _applicationName = applicationName;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;

        }

        public T GetValue<T>(string key)
        {
            T returnValue = base.Cacher.Get<T>(_applicationName + key);

            if (returnValue == null || returnValue.Equals(default(T)))
            {
                returnValue = GetConfigurationValueFromDb<T>(key);

                if (returnValue != null && !returnValue.Equals(default(T)))
                {
                    DateTime expireDate = DateTime.Now.AddMilliseconds(_refreshTimerIntervalInMs);
                    base.Cacher.Set<T>(_applicationName + key, returnValue, expireDate);
                }
            }

            return returnValue;
        }

        private T GetConfigurationValueFromDb<T>(string key)
        {
            ConfigurationObject configurationObject = this.Repo.GetByNames(key, _applicationName);

            return base.ConvertToType<T>(configurationObject);
        }
    }
}
