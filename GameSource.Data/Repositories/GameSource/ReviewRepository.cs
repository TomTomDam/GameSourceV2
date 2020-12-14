using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Data.Repositories.GameSource
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private GameSource_DBContext context;
        private DbSet<Review> entity;

        public ReviewRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Review>();
        }
    }
}
