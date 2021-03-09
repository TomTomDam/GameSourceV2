using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private IPlatformTypeRepository repo;

        public PlatformTypeService(IPlatformTypeRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<PlatformType> GetAll()
        {
            return repo.GetAll();
        }

        public PlatformType GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(PlatformType platformType)
        {
            repo.Insert(platformType);
        }

        public void Update(PlatformType platformType)
        {
            repo.Update(platformType);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Task<IEnumerable<PlatformType>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PlatformType> GetByIDAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(PlatformType platformType)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(PlatformType platformType)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
