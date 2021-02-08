using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        private GameSource_DBContext context;
        private DbSet<Publisher> entity;

        public PublisherRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Publisher>();
        }

        public IEnumerable<Publisher> GetAll()
        {
            return entity.ToList();
        }

        public Publisher GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(Publisher publisher)
        {
            entity.Add(publisher);
            context.SaveChanges();
        }

        public void Update(Publisher publisher)
        {
            entity.Update(publisher);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var publisher = GetByID(id);
            entity.Remove(publisher);
            context.SaveChanges();
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<Publisher> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task InsertAsync(Publisher publisher)
        {
            entity.Add(publisher);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Publisher publisher)
        {
            entity.Update(publisher);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await GetByIDAsync(id);
            entity.Remove(publisher);
            await context.SaveChangesAsync();
        }
    }
}
