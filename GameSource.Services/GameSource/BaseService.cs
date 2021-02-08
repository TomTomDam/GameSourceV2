using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public IBaseRepository<T> repo;

        public BaseService(IBaseRepository<T> repo)
        {
            this.repo = repo;
        }

        public IEnumerable<T> GetAll()
        {
            return repo.GetAll();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public T GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public void Insert(T item)
        {
            repo.Insert(item);
        }

        public async Task InsertAsync(T item)
        {
            await repo.InsertAsync(item);
        }

        public void Update(T item)
        {
            repo.Update(item);
        }

        public async Task UpdateAsync(T item)
        {
            await repo.UpdateAsync(item);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
