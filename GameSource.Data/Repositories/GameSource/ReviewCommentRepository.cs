using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameSource.Data.Repositories.GameSource
{
    public class ReviewCommentRepository : BaseRepository<ReviewComment>, IReviewCommentRepository
    {
        private GameSource_DBContext context;
        private DbSet<ReviewComment> entity;

        public ReviewCommentRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<ReviewComment>();
        }

        public ReviewComment GetByID(int id)
        {
            return entity
                .Include(x => x.Review)
                .SingleOrDefault(x => x.ID == id);
        }
    }
}
