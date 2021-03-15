using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class PlatformTypeRepository : BaseRepository<PlatformType>, IPlatformTypeRepository
    {
        private DbSet<PlatformType> repo => context.Set<PlatformType>();

        public PlatformTypeRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
