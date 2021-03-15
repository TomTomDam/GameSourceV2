using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        protected DbSet<Publisher> repo => context.Set<Publisher>();

        public PublisherRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
