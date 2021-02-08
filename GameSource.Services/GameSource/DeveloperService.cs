using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class DeveloperService : IDeveloperService
    {
        private IDeveloperRepository repo;

        public DeveloperService(IDeveloperRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Developer> GetAll()
        {
            return repo.GetAll();
        }

        public Developer GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Developer developer)
        {
            repo.Insert(developer);
        }

        public void Update(Developer developer)
        {
            repo.Update(developer);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<Developer> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(Developer developer)
        {
            await repo.InsertAsync(developer);
        }

        public async Task UpdateAsync(Developer developer)
        {
            await repo.UpdateAsync(developer);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
