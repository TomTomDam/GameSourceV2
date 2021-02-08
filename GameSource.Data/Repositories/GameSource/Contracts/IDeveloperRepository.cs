using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IDeveloperRepository
    {
        public IEnumerable<Developer> GetAll();
        public Developer GetByID(int id);
        public void Insert(Developer developer);
        public void Update(Developer developer);
        public void Delete(int id);
        public Task<IEnumerable<Developer>> GetAllAsync();
        public Task<Developer> GetByIDAsync(int id);
        public Task InsertAsync(Developer developer);
        public Task UpdateAsync(Developer developer);
        public Task DeleteAsync(int id);
    }
}
