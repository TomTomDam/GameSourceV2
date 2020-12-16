using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IReviewCommentRepository
    {
        public IEnumerable<ReviewComment> GetAll();
        public ReviewComment GetByID(int id);
        public void Insert(ReviewComment review);
        public void Update(ReviewComment review);
        public void Delete(int id);
        public Review GetReview(ReviewComment comment);
    }
}
