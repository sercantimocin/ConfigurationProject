using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Core.Data
{
    public interface IDataHelper
    {
        IDbConnection GetOpenConnection(string connectionString);

        Task<int> ExecuteAsync(string connectionString, string sql, object param);

        T Query<T>(string connectionString, string sql, object param);

        Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql);
    }
}
