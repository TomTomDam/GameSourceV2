using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class PlatformService : BaseService<Platform>, IPlatformService
    {
        private DbSet<Platform> repo => context.Set<Platform>();

        public PlatformService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
