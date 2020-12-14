using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Services.GameSource.Contracts
{
    public class ReviewCommentService : IReviewCommentService
    {
        public IReviewCommentRepository repo;

        public ReviewCommentService(IReviewCommentRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<ReviewComment> GetAll()
        {
            return repo.GetAll();
        }

        public ReviewComment GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(ReviewComment reviewComment)
        {
            repo.Insert(reviewComment);
        }

        public void Update(ReviewComment reviewComment)
        {
            repo.Update(reviewComment);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
