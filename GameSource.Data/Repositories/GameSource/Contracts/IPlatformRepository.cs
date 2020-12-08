using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IPlatformRepository
    {
        public IEnumerable<Platform> GetAll();
        public Platform GetByID(int id);
        public void Insert(Platform platform);
        public void Update(Platform platform);
        public void Delete(int id);
        public Task<IEnumerable<Platform>> GetAllAsync();
        public Task<Platform> GetByIDAsync(int id);
        public Task InsertAsync(Platform platform);
        public Task UpdateAsync(Platform platform);
        public Task DeleteAsync(int id);
    }
}
