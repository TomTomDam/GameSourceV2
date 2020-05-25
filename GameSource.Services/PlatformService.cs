using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using GameSource.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services
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
