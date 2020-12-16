using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;

namespace GameSource.Services.GameSource
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository repo;

        public ReviewService(IReviewRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Review> GetAll()
        {
            return repo.GetAll();
        }

        public Review GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Review review)
        {
            repo.Insert(review);
        }

        public void Update(Review review)
        {
            repo.Update(review);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public IEnumerable<ReviewComment> GetReviewComments(Review review)
        {
            return repo.GetReviewComments(review);
        }
    }
}
