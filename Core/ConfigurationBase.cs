using Core.Cache;
using Data.Repository;
using Model;

namespace Core
{
    public class ConfigurationBase
    {
        protected readonly ICacher Cacher;
        protected readonly IConfigurationRepository Repo;

        public ConfigurationBase(ICacher cacher, IConfigurationRepository repo, string connectionString)
        {
            this.Cacher = cacher;
            this.Repo = repo;
            this.Repo.ConnectionString = connectionString;

        }

        public T ConvertToType<T>(ConfigurationObject configurationObject)
        {
            if (configurationObject == null)
            {
                return default(T);
            }

            object returnValue = configurationObject.Value;

            string lowerTypeName = configurationObject.Type.ToLowerInvariant();

            if (lowerTypeName == "boolean")
            {
                returnValue = configurationObject.Value == "1";
            }

            if (lowerTypeName == "int")
            {
                returnValue = int.Parse(configurationObject.Value);
            }

            if (lowerTypeName == "double")
            {
                returnValue = double.Parse(configurationObject.Value);
            }

            return (T)returnValue;
        }
    }
}
