using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository repo;

        public PublisherService(IPublisherRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return repo.GetAll();
        }

        public Publisher GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Publisher publisher)
        {
            repo.Insert(publisher);
        }

        public void Update(Publisher publisher)
        {
            repo.Update(publisher);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<Publisher> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(Publisher publisher)
        {
            await repo.InsertAsync(publisher);
        }

        public async Task UpdateAsync(Publisher publisher)
        {
            await repo.UpdateAsync(publisher);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
