using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<ReviewComment> GetReviewComments(Review review)
        {
            Review reviewWithComments = entity
                .Include(x => x.ReviewComments)
                .SingleOrDefault(y => y.ID == review.ID);

            return reviewWithComments.ReviewComments;
        }
    }
}
