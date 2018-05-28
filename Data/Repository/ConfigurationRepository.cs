using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data;
using Model;

namespace Data.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IDataHelper _dataHelper;
        public ConfigurationRepository(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }
        public string ConnectionString { private get; set; }

        public async Task<IEnumerable<ConfigurationObject>> GetAllAsync()
        {
            var query = "SELECT [Id],[Name],[Type],[Value],[IsActive],[ApplicationName] FROM [dbo].[Configurations]";

            return await this._dataHelper.QueryAsync<ConfigurationObject>(this.ConnectionString, query);
        }

        public ConfigurationObject GetByNames(string key, string applicationName)
        {
            string query = "SELECT Value,Type FROM Configurations WHERE IsActive = 1 AND ApplicationName = @ApplicationName AND Name = @Key";

            return _dataHelper.Query<ConfigurationObject>(this.ConnectionString, query, new { applicationName, key });
        }

        public async Task<int> InsertAsync(ConfigurationObject configurationObject)
        {
            string query = "INSERT INTO dbo.Configurations VALUES (@Name, @Type, @Value, @IsActive, @ApplicationName)";

            return await _dataHelper.ExecuteAsync(this.ConnectionString, query,
                new
                {
                    configurationObject.Name,
                    configurationObject.Type,
                    configurationObject.Value,
                    configurationObject.IsActive,
                    configurationObject.ApplicationName
                });
        }

        public async Task<int> UpdateAsync(ConfigurationObject configurationObject)
        {
            string query = @"UPDATE dbo.Configurations
                                SET Name = @Name, Type = @Type , Value = @Value, IsActive = @IsActive, ApplicationName = @ApplicationName
                              WHERE Id = @Id";

            return await _dataHelper.ExecuteAsync(this.ConnectionString, query,
                new
                {
                    configurationObject.Id,
                    configurationObject.Name,
                    configurationObject.Type,
                    configurationObject.Value,
                    configurationObject.IsActive,
                    configurationObject.ApplicationName
                });
        }

        public ConfigurationObject FindById(int id)
        {
            var query = "SELECT [Id],[Name],[Type],[Value],[IsActive],[ApplicationName] FROM [dbo].[Configurations](NOLOCK) WHERE Id = @Id";

            return this._dataHelper.Query<ConfigurationObject>(this.ConnectionString, query,new {id});
        }
    }
}