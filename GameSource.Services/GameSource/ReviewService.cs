using GameSource.Infrastructure;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSource
{
    public class ReviewService : BaseService<Review>, IReviewService
    {
        private readonly GameSource_DBContext context;
        private DbSet<Game> repo => context.Set<Game>();

        public ReviewService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
