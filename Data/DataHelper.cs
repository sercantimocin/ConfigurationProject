using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Dapper;

namespace Data
{
    public class Disposable
    {
        public static TResult Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, TResult> map) where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return map(disposable);
            }
        }

        public static async Task<TResult> UsingAsync<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, Task<TResult>> map) where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return await map(disposable);
            }
        }
    }

    /// <summary>
    /// The data helper.
    /// </summary>
    public class DataHelper : IDataHelper
    {
        public IDbConnection GetOpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public Task<int> ExecuteAsync(string connectionString, string sql, object param)
        {
            return Disposable.UsingAsync(() => this.GetOpenConnection(connectionString), async connection => await connection.ExecuteAsync(sql, param));
        }

        public T Query<T>(string connectionString, string sql, object param)
        {
            return Disposable.Using(() => this.GetOpenConnection(connectionString), connection => connection.Query<T>(sql, param).FirstOrDefault());
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql)
        {
            return await this.QueryAsync<T>(connectionString, sql, null);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string connectionString, string sql, object param)
        {
            return Disposable.UsingAsync(() => this.GetOpenConnection(connectionString), async connection => await connection.QueryAsync<T>(sql, param));
        }
    }
}