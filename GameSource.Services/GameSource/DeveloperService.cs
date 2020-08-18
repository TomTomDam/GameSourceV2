using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;

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
    }
}
