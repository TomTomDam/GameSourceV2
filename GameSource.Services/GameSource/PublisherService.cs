using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class PublisherService : BaseService<Publisher>, IPublisherService
    {
        private readonly GameSource_DBContext context;
        private DbSet<Publisher> repo => context.Set<Publisher>();

        public PublisherService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
