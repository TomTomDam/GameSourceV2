using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource
{
    public class PlatformRepository : BaseRepository<Platform>, IPlatformRepository
    {
        private GameSource_DBContext context;
        private DbSet<Platform> entity; 

        public PlatformRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Platform>();
        }

        public IEnumerable<Platform> GetAll()
        {
            return entity.ToList();
        }

        public Platform GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(Platform platform)
        {
            entity.Add(platform);
            context.SaveChanges();
        }

        public void Update(Platform platform)
        {
            entity.Update(platform);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Platform platform = GetByID(id);
            entity.Remove(platform);
            context.SaveChanges();
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<Platform> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task InsertAsync(Platform platform)
        {
            await entity.AddAsync(platform);
        }

        public async Task UpdateAsync(Platform platform)
        {
            entity.Update(platform);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Platform platform = await GetByIDAsync(id);
            entity.Remove(platform);
            await context.SaveChangesAsync();
        }
    }
}
