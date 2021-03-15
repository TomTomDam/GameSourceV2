using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSource
{
    public class ReviewCommentRepository : BaseRepository<ReviewComment>, IReviewCommentRepository
    {
        private DbSet<ReviewComment> repo => context.Set<ReviewComment>();

        public ReviewCommentRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
