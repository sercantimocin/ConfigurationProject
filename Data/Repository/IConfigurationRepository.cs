using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace Data.Repository
{
    public interface IConfigurationRepository
    {
        string ConnectionString {set; }

        Task<IEnumerable<ConfigurationObject>> GetAllAsync();

        ConfigurationObject GetByNames(string key, string applicationName);

        Task<int> InsertAsync(ConfigurationObject configurationObject);

        Task<int> UpdateAsync(ConfigurationObject configurationObject);

        ConfigurationObject FindById(int id);
    }
}
