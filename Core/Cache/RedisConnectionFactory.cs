using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace ConsoleApp2
{
    public class RedisConnectionFactory
    {
        private Lazy<ConnectionMultiplexer> Connection;

        private static string REDIS_CONNECTIONSTRING = "REDIS_CONNECTIONSTRING";
        private static RedisConnectionFactory _instance;

        private RedisConnectionFactory()
        {
            string connectionString = Environment.GetEnvironmentVariable(REDIS_CONNECTIONSTRING, EnvironmentVariableTarget.Machine);

            if (connectionString == null)
            {
                throw new KeyNotFoundException($"Environment variable for {REDIS_CONNECTIONSTRING} was not found.");
            }

            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);

            Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }

        public static ConnectionMultiplexer GetConnection()
        {
            if (_instance == null)
            {
                _instance = new RedisConnectionFactory();
            }

            return _instance.Connection.Value;
        }
    }
}
