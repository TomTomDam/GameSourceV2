using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetByID(int id);
        public int Insert(T item);
        public int Update(T item);
        public int Delete(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIDAsync(int id);
        public Task<int> InsertAsync(T item);
        public Task<int> UpdateAsync(T item);
        public Task<int> DeleteAsync(int id);
    }
}
