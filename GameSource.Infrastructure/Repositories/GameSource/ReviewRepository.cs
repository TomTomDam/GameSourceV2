using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private DbSet<Game> repo => context.Set<Game>();

        public ReviewRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
