using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class PlatformRepository : BaseRepository<Platform>, IPlatformRepository
    {
        private DbSet<Platform> repo => context.Set<Platform>();

        public PlatformRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
