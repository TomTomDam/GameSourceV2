using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IPublisherService
    {
        public IEnumerable<Publisher> GetAll();
        public Publisher GetByID(int id);
        public void Insert(Publisher publisher);
        public void Update(Publisher publisher);
        public void Delete(int id);
        public Task<IEnumerable<Publisher>> GetAllAsync();
        public Task<Publisher> GetByIDAsync(int id);
        public Task InsertAsync(Publisher publisher);
        public Task UpdateAsync(Publisher publisher);
        public Task DeleteAsync(int id);
    }
}
