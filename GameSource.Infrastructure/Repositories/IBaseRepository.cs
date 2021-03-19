using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetByID(int id);
        public bool Insert(T item);
        public bool Update(T item);
        public bool Delete(T item);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIDAsync(int id);
        public Task<bool> InsertAsync(T item);
        public Task<bool> UpdateAsync(T item);
        public Task<bool> DeleteAsync(T item);
    }
}
