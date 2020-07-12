using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using GameSource.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services
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
    }
}
