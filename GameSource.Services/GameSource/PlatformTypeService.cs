using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class PlatformTypeService : BaseService<PlatformType>, IPlatformTypeService
    {
        private readonly GameSource_DBContext context;
        private DbSet<PlatformType> repo => context.Set<PlatformType>();

        public PlatformTypeService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
