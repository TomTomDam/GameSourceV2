using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class DeveloperService : BaseService<Developer>, IDeveloperService
    {
        private readonly GameSource_DBContext context;
        private DbSet<Developer> repo => context.Set<Developer>();

        public DeveloperService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
