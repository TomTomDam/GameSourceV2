using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IDeveloperService
    {
        public IEnumerable<Developer> GetAll();
        public Developer GetByID(int id);
        public void Insert(Developer developer);
        public void Update(Developer developer);
        public void Delete(int id);
    }
}
