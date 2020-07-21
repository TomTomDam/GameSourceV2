using GameSource.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories
{
    public interface IDapperRepository
    {
        Task<int?> InsertAsync<T>(T model) where T : DataEntity;
        Task<T> GetAsync<T>(int id) where T : DataEntity;
        Task<List<T>> GetAllAsync<T>();
        Task<List<T>> GetAllAsync<T>(string filter, object args);
        Task<List<T>> GetAllAsync<T>(string filter, string orderBy, object args);
        Task<int> UpdateAsync<T>(T model) where T : DataEntity;
        Task<int> DeleteAsync<T>(int id) where T : DataEntity;
        Task<int> DeleteAsync<T>(T model) where T : DataEntity;
        Task<bool> ExistsAsync<T>(int id) where T : DataEntity;
    }
}
