using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        public T GetByID(int id);
        public IEnumerable<T> GetAll();
        public void Insert(T item);
        public void Update(T item);
        public void Delete(int id);
        public Task<T> GetByIDAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task InsertAsync(T item);
        public Task UpdateAsync(T item);
        public Task DeleteAsync(int id);
    }
}
