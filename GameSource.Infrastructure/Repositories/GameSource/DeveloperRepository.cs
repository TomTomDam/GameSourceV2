using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class DeveloperRepository : BaseRepository<Developer>, IDeveloperRepository
    {
        private DbSet<Developer> repo => context.Set<Developer>();

        public DeveloperRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> InsertBoolAsync(Developer item)
        {
            await entity.AddAsync(item);
            var inserted = await context.SaveChangesAsync();
            return inserted > 0;
        }
    }
}
