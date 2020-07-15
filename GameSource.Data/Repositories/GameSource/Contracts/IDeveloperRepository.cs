using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IDeveloperRepository
    {
        public IEnumerable<Developer> GetAll();
        public Developer GetByID(int id);
        public void Insert(Developer developer);
        public void Update(Developer developer);
        public void Delete(int id);
    }
}
