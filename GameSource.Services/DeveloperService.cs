using GameSource.Data.Repositories;
using GameSource.Models;
using GameSource.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services
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
