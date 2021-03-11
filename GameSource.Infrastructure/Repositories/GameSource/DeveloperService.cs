using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class DeveloperRepository : BaseRepository<Developer>, IDeveloperRepository
    {
        private DbSet<Developer> repo => context.Set<Developer>();

        public DeveloperRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
