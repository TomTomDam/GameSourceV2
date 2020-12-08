using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class PlatformService : IPlatformService
    {
        private IPlatformRepository repo;

        public PlatformService(IPlatformRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Platform> GetAll()
        {
            return repo.GetAll();
        }

        public Platform GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Platform platform)
        {
            repo.Insert(platform);
        }

        public void Update(Platform platform)
        {
            repo.Update(platform);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<Platform> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(Platform platform)
        {
            await repo.InsertAsync(platform);
        }

        public async Task UpdateAsync(Platform platform)
        {
            await repo.UpdateAsync(platform);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
