using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class ReviewCommentService : BaseService<ReviewComment>, IReviewCommentService
    {
        private DbSet<ReviewComment> repo => context.Set<ReviewComment>();

        public ReviewCommentService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
