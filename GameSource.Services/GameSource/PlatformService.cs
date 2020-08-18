using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;

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
    }
}
