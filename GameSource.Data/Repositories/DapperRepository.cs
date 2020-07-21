using Dapper;
using GameSource.Data.Settings;
using GameSource.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories
{
    public class DapperRepository : IDapperRepository
    {
        private readonly DatabaseSettings dbSettings;

        public DapperRepository(IOptions<DatabaseSettings> dbSettings)
        {
            this.dbSettings = dbSettings.Value;
        }

        public async Task<T> GetAsync<T>(int id) where T : DataEntity
        {
            string tableName = GetTableName<T>();
            string sql = $"SELECT * FROM {tableName} WHERE Id = @id";
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return await connection.QuerySingleAsync<T>(sql, new { id });
            }
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            string tableName = GetTableName<T>();
            string sql = $"SELECT * FROM {tableName}";

            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return (await connection.QueryAsync<T>(sql)).ToList();
            }
        }

        public async Task<List<T>> GetAllAsync<T>(string filter, object args)
        {
            string tableName = GetTableName<T>();
            string sql = $"SELECT * FROM {tableName} WHERE {filter}";

            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return (await connection.QueryAsync<T>(sql, args)).ToList();
            }
        }

        public async Task<List<T>> GetAllAsync<T>(string filter, string orderBy, object args)
        {
            string tableName = GetTableName<T>();
            string sql = $"SELECT * FROM {tableName} WHERE {filter} ORDER BY {orderBy}";

            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return (await connection.QueryAsync<T>(sql, args)).ToList();
            }
        }

        public async Task<int?> InsertAsync<T>(T model) where T : DataEntity
        {
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return await connection.InsertAsync(model);
            }
        }

        public async Task<int> UpdateAsync<T>(T model) where T : DataEntity
        {
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return await connection.UpdateAsync(model);
            }
        }

        public async Task<int> DeleteAsync<T>(int id) where T : DataEntity
        {
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return await connection.DeleteAsync(id);
            }
        }

        public async Task<int> DeleteAsync<T>(T model) where T : DataEntity
        {
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return await connection.DeleteAsync(model);
            }
        }

        public async Task<bool> ExistsAsync<T>(int id) where T : DataEntity
        {
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return await connection.RecordCountAsync<T>("WHERE Id=@id", new { id }) > 0;
            }
        }

        private static string GetTableName<T>()
        {
            var tableAttrib = typeof(T).GetCustomAttributes(true)
                .SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
            string tableName = tableAttrib?.Name ?? typeof(T).Name + "s";
            return tableName;
        }

        protected async Task<List<T>> QueryAsync<T>(string sql, object args)
        {
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                return (await connection.QueryAsync<T>(sql, args)).ToList();
            }
        }
    }
}
